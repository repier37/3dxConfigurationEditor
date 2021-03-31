using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _3DxConfigurationEditor
{
    public class KeyWithAction
    {
        public KeyWithAction (Key inKey, KeyAction inAction)
        {
            this.Action = inAction;
            this.Key = inKey;
        }

        public KeyAction Action{ get; set; }

        public Key Key { get; set; }

        internal string GetXMLElementName()
        {
            switch (this.Action)
            {
                case KeyAction.Pressed:
                    return "KeyPress";
                case KeyAction.Released:
                    return "KeyRelease";
                default:
                    return string.Empty;
            }
        }

        public string GetHIDValue()
        {
            int result = 0;
            switch (this.Key)
            {
                case Key.None:
                    result = HIDTable.KEY_NONE;
                    break;
                case Key.Cancel:
                    break;
                case Key.Back:
                    result = HIDTable.KEY_BACKSPACE;
                    break;
                case Key.Tab:
                    result = HIDTable.KEY_TAB;
                    break;
                case Key.LineFeed:
                    result = HIDTable.KEY_ENTER;
                    break;
                case Key.Return:
                    result = HIDTable.KEY_ENTER;
                    break;
                case Key.Pause:
                    result = HIDTable.KEY_PAUSE;
                    break;
                case Key.Capital:
                    result = HIDTable.KEY_CAPSLOCK;
                    break;
                case Key.Escape:
                    result = HIDTable.KEY_ESC;
                    break;
                case Key.Space:
                    result = HIDTable.KEY_SPACE;
                    break;
                case Key.PageUp:
                    result = HIDTable.KEY_PAGEUP;
                    break;
                case Key.PageDown:
                    result = HIDTable.KEY_DOWN;
                    break;
                case Key.End:
                    result = HIDTable.KEY_END;
                    break;
                case Key.Home:
                    result = HIDTable.KEY_HOME;
                    break;
                case Key.Left:
                    result = HIDTable.KEY_LEFT;
                    break;
                case Key.Up:
                    result = HIDTable.KEY_UP;
                    break;
                case Key.Right:
                    result = HIDTable.KEY_RIGHT;
                    break;
                case Key.Down:
                    result = HIDTable.KEY_DOWN;
                    break;
                case Key.Select:
                    result = HIDTable.KEY_FRONT;
                    break;
                case Key.Print:
                    break;
                case Key.Execute:
                    result = HIDTable.KEY_OPEN;
                    break;
                case Key.PrintScreen:
                    result = HIDTable.KEY_SYSRQ;
                    break;
                case Key.Insert:
                    result = HIDTable.KEY_INSERT;
                    break;
                case Key.Delete:
                    result = HIDTable.KEY_DELETE;
                    break;
                case Key.Help:
                    result = HIDTable.KEY_HELP;
                    break;
                case Key.D0:
                    result = HIDTable.KEY_0;
                    break;
                case Key.D1:
                    result = HIDTable.KEY_1;
                    break;
                case Key.D2:
                    result = HIDTable.KEY_2;
                    break;
                case Key.D3:
                    result = HIDTable.KEY_3;
                    break;
                case Key.D4:
                    result = HIDTable.KEY_4;
                    break;
                case Key.D5:
                    result = HIDTable.KEY_5;
                    break;
                case Key.D6:
                    result = HIDTable.KEY_6;
                    break;
                case Key.D7:
                    result = HIDTable.KEY_7;
                    break;
                case Key.D8:
                    result = HIDTable.KEY_8;
                    break;
                case Key.D9:
                    result = HIDTable.KEY_9;
                    break;
                case Key.A:
                    result = HIDTable.KEY_A;
                    break;
                case Key.B:
                    result = HIDTable.KEY_B;
                    break;
                case Key.C:
                    result = HIDTable.KEY_C;
                    break;
                case Key.D:
                    result = HIDTable.KEY_D;
                    break;
                case Key.E:
                    result = HIDTable.KEY_E;
                    break;
                case Key.F:
                    result = HIDTable.KEY_F;
                    break;
                case Key.G:
                    result = HIDTable.KEY_G;
                    break;
                case Key.H:
                    result = HIDTable.KEY_H;
                    break;
                case Key.I:
                    result = HIDTable.KEY_I;
                    break;
                case Key.J:
                    result = HIDTable.KEY_J;
                    break;
                case Key.K:
                    result = HIDTable.KEY_K;
                    break;
                case Key.L:
                    result = HIDTable.KEY_L;
                    break;
                case Key.M:
                    result = HIDTable.KEY_M;
                    break;
                case Key.N:
                    result = HIDTable.KEY_N;
                    break;
                case Key.O:
                    result = HIDTable.KEY_O;
                    break;
                case Key.P:
                    result = HIDTable.KEY_P;
                    break;
                case Key.Q:
                    result = HIDTable.KEY_Q;
                    break;
                case Key.R:
                    result = HIDTable.KEY_R;
                    break;
                case Key.S:
                    result = HIDTable.KEY_S;
                    break;
                case Key.T:
                    result = HIDTable.KEY_T;
                    break;
                case Key.U:
                    result = HIDTable.KEY_U;
                    break;
                case Key.V:
                    result = HIDTable.KEY_V;
                    break;
                case Key.W:
                    result = HIDTable.KEY_W;
                    break;
                case Key.X:
                    result = HIDTable.KEY_X;
                    break;
                case Key.Y:
                    result = HIDTable.KEY_Y;
                    break;
                case Key.Z:
                    result = HIDTable.KEY_Z;
                    break;
                case Key.LWin:
                    result = HIDTable.KEY_LEFTMETA;
                    break;
                case Key.RWin:
                    result = HIDTable.KEY_RIGHTMETA;
                    break;
                case Key.Apps:
                    break;
                case Key.Sleep:
                    result = HIDTable.KEY_MEDIA_SLEEP;
                    break;
                case Key.NumPad0:
                    result = HIDTable.KEY_KP0;
                    break;
                case Key.NumPad1:
                    result = HIDTable.KEY_KP1;
                    break;
                case Key.NumPad2:
                    result = HIDTable.KEY_KP2;
                    break;
                case Key.NumPad3:
                    result = HIDTable.KEY_KP3;
                    break;
                case Key.NumPad4:
                    result = HIDTable.KEY_KP4;
                    break;
                case Key.NumPad5:
                    result = HIDTable.KEY_KP5;
                    break;
                case Key.NumPad6:
                    result = HIDTable.KEY_KP6;
                    break;
                case Key.NumPad7:
                    result = HIDTable.KEY_KP7;
                    break;
                case Key.NumPad8:
                    result = HIDTable.KEY_KP8;
                    break;
                case Key.NumPad9:
                    result = HIDTable.KEY_KP9;
                    break;
                case Key.Multiply:
                    result = HIDTable.KEY_KPASTERISK;
                    break;
                case Key.Add:
                    result = HIDTable.KEY_KPPLUS;
                    break;
                case Key.Separator:
                    result = HIDTable.KEY_KPCOMMA; //TODO NOT SURE
                    break;
                case Key.Subtract:
                    result = HIDTable.KEY_KPMINUS;
                    break;
                case Key.Decimal:
                    result = HIDTable.KEY_KPDOT;//TODO NOT SURE
                    break;
                case Key.Divide:
                    result = HIDTable.KEY_KPSLASH;
                    break;
                case Key.F1:
                    result = HIDTable.KEY_F1;
                    break;
                case Key.F2:
                    result = HIDTable.KEY_F2;
                    break;
                case Key.F3:
                    result = HIDTable.KEY_F3;
                    break;
                case Key.F4:
                    result = HIDTable.KEY_F4;
                    break;
                case Key.F5:
                    result = HIDTable.KEY_F5;
                    break;
                case Key.F6:
                    result = HIDTable.KEY_F6;
                    break;
                case Key.F7:
                    result = HIDTable.KEY_F7;
                    break;
                case Key.F8:
                    result = HIDTable.KEY_F8;
                    break;
                case Key.F9:
                    result = HIDTable.KEY_F9;
                    break;
                case Key.F10:
                    result = HIDTable.KEY_F10;
                    break;
                case Key.F11:
                    result = HIDTable.KEY_F11;
                    break;
                case Key.F12:
                    result = HIDTable.KEY_F12;
                    break;
                case Key.F13:
                    result = HIDTable.KEY_F13;
                    break;
                case Key.F14:
                    result = HIDTable.KEY_F14;
                    break;
                case Key.F15:
                    result = HIDTable.KEY_F15;
                    break;
                case Key.F16:
                    result = HIDTable.KEY_F16;
                    break;
                case Key.F17:
                    result = HIDTable.KEY_F17;
                    break;
                case Key.F18:
                    result = HIDTable.KEY_F18;
                    break;
                case Key.F19:
                    result = HIDTable.KEY_F19;
                    break;
                case Key.F20:
                    result = HIDTable.KEY_F20;
                    break;
                case Key.F21:
                    result = HIDTable.KEY_F21;
                    break;
                case Key.F22:
                    result = HIDTable.KEY_F22;
                    break;
                case Key.F23:
                    result = HIDTable.KEY_F23;
                    break;
                case Key.F24:
                    result = HIDTable.KEY_F24;
                    break;
                case Key.NumLock:
                    result = HIDTable.KEY_NUMLOCK;
                    break;
                case Key.Scroll:
                    result = HIDTable.KEY_SCROLLLOCK;
                    break;
                case Key.LeftShift:
                    result = HIDTable.KEY_LEFTSHIFT;
                    break;
                case Key.RightShift:
                    result = HIDTable.KEY_RIGHTSHIFT;
                    break;
                case Key.LeftCtrl:
                    result = HIDTable.KEY_LEFTCTRL;
                    break;
                case Key.RightCtrl:
                    result = HIDTable.KEY_RIGHTCTRL;
                    break;
                case Key.LeftAlt:
                    result = HIDTable.KEY_LEFTALT;
                    break;
                case Key.RightAlt:
                    result = HIDTable.KEY_RIGHTALT;
                    break;
                case Key.BrowserBack:
                    result = HIDTable.KEY_MEDIA_BACK;
                    break;
                case Key.BrowserForward:
                    result = HIDTable.KEY_MEDIA_FORWARD;
                    break;
                case Key.BrowserRefresh:
                    result = HIDTable.KEY_MEDIA_REFRESH;
                    break;
                case Key.BrowserStop:
                    break;
                case Key.BrowserSearch:
                    result = HIDTable.KEY_MEDIA_FIND;
                    break;
                case Key.BrowserFavorites:
                    result = HIDTable.KEY_A;
                    break;
                case Key.BrowserHome:
                    result = HIDTable.KEY_A;
                    break;
                case Key.VolumeMute:
                    result = HIDTable.KEY_MEDIA_MUTE;
                    break;
                case Key.VolumeDown:
                    result = HIDTable.KEY_MEDIA_VOLUMEDOWN;
                    break;
                case Key.VolumeUp:
                    result = HIDTable.KEY_MEDIA_VOLUMEUP;
                    break;
                case Key.MediaNextTrack:
                    result = HIDTable.KEY_MEDIA_NEXTSONG;
                    break;
                case Key.MediaPreviousTrack:
                    result = HIDTable.KEY_MEDIA_PREVIOUSSONG;
                    break;
                case Key.MediaStop:
                    result = HIDTable.KEY_MEDIA_STOP;
                    break;
                case Key.MediaPlayPause:
                    result = HIDTable.KEY_MEDIA_PLAYPAUSE;
                    break;
                case Key.LaunchMail:
                    break;
                case Key.SelectMedia:
                    break;
                case Key.OemSemicolon:
                    result = HIDTable.KEY_SEMICOLON;
                    break;
                case Key.OemPlus:
                    result = HIDTable.KEY_KPPLUS; //TODO NOT SURE
                    break;
                case Key.OemComma:
                    result = HIDTable.KEY_COMMA;
                    break;
                case Key.OemMinus:
                    result = HIDTable.KEY_MINUS;
                    break;
                case Key.OemPeriod:
                    break;
                case Key.OemQuestion:
                    break;
                case Key.OemTilde:
                    result = HIDTable.KEY_HASHTILDE;
                    break;
                case Key.OemOpenBrackets:
                    result = HIDTable.KEY_LEFTBRACE;
                    break;
                case Key.OemPipe:
                    break;
                case Key.OemCloseBrackets:
                    result = HIDTable.KEY_RIGHTBRACE;
                    break;
                case Key.OemQuotes:
                    break;
                case Key.Oem8:
                    break;
                case Key.OemBackslash:
                    result = HIDTable.KEY_BACKSLASH;
                    break;
                case Key.ImeProcessed:
                    break;
                case Key.System:
                    break;
                case Key.DbeAlphanumeric:
                    break;
                case Key.OemFinish:
                    break;
                case Key.OemCopy:
                    break;
                case Key.OemAuto:
                    break;
                case Key.OemEnlw:
                    break;
                case Key.OemBackTab:
                    break;
                case Key.Attn:
                    break;
                case Key.CrSel:
                    break;
                case Key.ExSel:
                    break;
                case Key.EraseEof:
                    break;
                case Key.Play:
                    break;
                case Key.Zoom:
                    break;
                case Key.NoName:
                    break;
                case Key.Pa1:
                    break;
                case Key.OemClear:
                    break;
                case Key.DeadCharProcessed:
                    break;
                default:
                    break;
            }


            if (result == 0)
                return string.Empty;

            return result.ToString("X");

            /*
            char upper = char.ToUpper(inChar);
            if (upper < 'A' || upper > 'Z')
            {
                throw new ArgumentOutOfRangeException("value", "This method only accepts standard Latin characters.");
            }

            //get the character position in alphabet, add 3 convert to hexa, then to string
            //char c = 'b'; you may use lower case character.
            int index = char.ToUpper(inChar) - 64;//index == 1
            index += 3;

            return index.ToString("X");
            */
        }
    }

    public enum KeyAction
    {
        Pressed=0,
        Released=1
    }
}
