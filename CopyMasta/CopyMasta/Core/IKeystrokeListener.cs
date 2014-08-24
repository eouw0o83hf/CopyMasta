using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyMasta.Core
{
    public abstract class KeystrokeListenerBase : IDisposable
    {
        public delegate void StateChanged(KeyState state);

        public event StateChanged OnStateChange;

        protected void FireChange(KeyState state)
        {
            if (OnStateChange != null)
            {
                OnStateChange(state);
            }
        }

        public abstract void Dispose();
    }

    public class KeystrokeLitener : KeystrokeListenerBase
    {
        private readonly KeyState _state;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        
        private const int WhKeyboardLl = 13;
        private static readonly IntPtr[] WmKeydowns = new [] { (IntPtr)0x0100, (IntPtr)0x0104 };
        private static readonly IntPtr[] WmKeyups = new[] { (IntPtr)0x0101, (IntPtr)0x0105 };
        private readonly LowLevelKeyboardProc _proc;
        private readonly IntPtr _hookId;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public KeystrokeLitener()
        {
            _hookId = IntPtr.Zero;
            _proc = HookCallback;

            _state = new KeyState
                {
                    Keys = new List<char>(),
                    MetaKeys = 0
                };

            _hookId = SetHook(_proc);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WhKeyboardLl, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            var previousState = _state.Clone();
            MetaKeys? metaChange = null;
            char? charChange = null;

            var vkCode = Marshal.ReadInt32(lParam);
            switch (vkCode)
            {
                case 0x09:
                    metaChange = MetaKeys.Tab;
                    break;
                    
                case 0x10:
                case 0xA0:
                case 0xA1:
                    metaChange = MetaKeys.Shift;
                    break;

                case 0x11:
                case 0xA2:
                case 0xA3:
                    metaChange = MetaKeys.Ctrl;
                    break;

                case 0x12:
                case 0xA4:
                case 0xA5:
                    metaChange = MetaKeys.Alt;
                    break;

                case 0x14:
                    metaChange = MetaKeys.CapsLock;
                    break;

                default:
                    var convert = new KeysConverter();
                    var asString = convert.ConvertToString(vkCode);

                    if (!string.IsNullOrWhiteSpace(asString))
                    {
                        charChange = asString[0];
                    }

                    break;
            }

            var isUp = WmKeyups.Contains(wParam);
            var isDown = WmKeydowns.Contains(wParam);

            if (metaChange.HasValue)
            {
                if (isUp && _state.MetaKeys.HasFlag(metaChange.Value))
                {
                    _state.MetaKeys ^= metaChange.Value;
                }
                else if (isDown)
                {
                    _state.MetaKeys |= metaChange.Value;
                }
            }
            if (charChange.HasValue)
            {
                if (isUp && _state.Keys.Contains(charChange.Value))
                {
                    _state.Keys.Remove(charChange.Value);
                }
                else if (isDown && !_state.Keys.Contains(charChange.Value))
                {
                    _state.Keys.Add(charChange.Value);
                }
            }

            if (!previousState.Equals(_state))
            {
                FireChange(_state);
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public override void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }
    }
}
