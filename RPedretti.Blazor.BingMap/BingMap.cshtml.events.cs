using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using RPedretti.Blazor.BingMap.Entities;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap
{
    public partial class BingMapBase : BlazorComponent
    {
        #region Fields

        private const string attachEventFunctionName = _mapNamespace + ".attachEvent";
        private const string attachChangeEventFunctionName = _mapNamespace + ".attachChangeEvent";
        private const string attachThrottleEventFunctionName = _mapNamespace + ".attachThrottleEvent";
        private const string detachEventFunctionName = _mapNamespace + ".detachEvent";
        private Func<MouseEventArgs<BingMapBase>, Task> _click;
        private Func<MouseEventArgs<BingMapBase>, Task> _doubleClick;
        private Func<MouseEventArgs<BingMapBase>, Task> _rightClick;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseMove;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseWheel;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseDown;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseOut;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseOver;
        private Func<MouseEventArgs<BingMapBase>, Task> _mouseUp;
        private Func<Task> _viewChangeStart;
        private Func<Task> _viewChange;
        private Func<Task> _viewChangeEnd;
        private Func<Task> _mapTypeChanged;
        private Func<Task> _throttleViewChangeStart;
        private Func<Task> _throttleViewChange;
        private Func<Task> _throttleViewChangeEnd;

        #endregion Fields

        #region Events

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> Click
        {
            get => _click;
            set
            {
                if (_click == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.Click, nameof(EmitMapEvent));
                }
                else if (_click != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.Click);
                }

                _click = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> DoubleClick
        {
            get => _doubleClick;
            set
            {
                if (_doubleClick == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.DoubleClick, nameof(EmitMapEvent));
                }
                else if (_doubleClick != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.DoubleClick);
                }

                _doubleClick = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> RightClick
        {
            get => _rightClick;
            set
            {
                if (_rightClick == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.RightClick, nameof(EmitMapEvent));
                }
                else if (_rightClick != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.RightClick);
                }

                _rightClick = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseMove
        {
            get => _mouseMove;
            set
            {
                if (_mouseMove == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseMove, nameof(EmitMapEvent));
                }
                else if (_mouseMove != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseMove);
                }

                _mouseMove = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseWheel
        {
            get => _mouseWheel;
            set
            {
                if (_mouseWheel == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseWheel, nameof(EmitMapEvent));
                }
                else if (_mouseWheel != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseWheel);
                }

                _mouseWheel = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseDown
        {
            get => _mouseDown;
            set
            {
                if (_mouseDown == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseDown, nameof(EmitMapEvent));
                }
                else if (_mouseDown != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseDown);
                }

                _mouseDown = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseOut
        {
            get => _mouseOut;
            set
            {
                if (_mouseOut == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseOut, nameof(EmitMapEvent));
                }
                else if (_mouseOut != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseOut);
                }

                _mouseOut = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseOver
        {
            get => _mouseOver;
            set
            {
                if (_mouseOver == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseOver, nameof(EmitMapEvent));
                }
                else if (_mouseOver != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseOver);
                }

                _mouseOver = value;
            }
        }

        [Parameter]
        protected Func<MouseEventArgs<BingMapBase>, Task> MouseUp
        {
            get => _mouseUp;
            set
            {
                if (_mouseUp == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseUp, nameof(EmitMapEvent));
                }
                else if (_mouseUp != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseUp);
                }

                _mouseUp = value;
            }
        }

        [Parameter]
        protected Func<Task> ViewChangeStart
        {
            get => _viewChangeStart;
            set
            {
                if (_viewChangeStart == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChangeStart, nameof(EmitMapChangeEvent));
                }
                else if (_viewChangeStart != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeStart);
                }

                _viewChangeStart = value;
            }
        }

        [Parameter]
        protected Func<Task> ViewChangeEnd
        {
            get => _viewChangeEnd;
            set
            {
                if (_viewChangeEnd == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChangeEnd, nameof(EmitMapChangeEvent));
                }
                else if (_viewChangeEnd != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeEnd);
                }

                _viewChangeEnd = value;
            }
        }

        [Parameter]
        protected Func<Task> ViewChange
        {
            get => _viewChange;
            set
            {
                if (_viewChange == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChange, nameof(EmitMapChangeEvent));
                }
                else if (_viewChange != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChange);
                }

                _viewChange = value;
            }
        }

        [Parameter]
        protected Func<Task> MapTypeChanged
        {
            get => _mapTypeChanged;
            set
            {
                if (_mapTypeChanged == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.MapTypeChanged, nameof(EmitMapChangeEvent));
                }
                else if (_mapTypeChanged != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MapTypeChanged);
                }

                _mapTypeChanged = value;
            }
        }

        [Parameter]
        protected Func<Task> ThrottleViewChangeStart
        {
            get => _throttleViewChangeStart;
            set
            {
                if (_throttleViewChangeStart == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChangeStart, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChangeStart != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseUp);
                }

                _throttleViewChangeStart = value;
            }
        }

        [Parameter]
        protected Func<Task> ThrottleViewChangeEnd
        {
            get => _throttleViewChangeEnd;
            set
            {
                if (_throttleViewChangeEnd == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChangeEnd, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChangeEnd != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeEnd);
                }

                _throttleViewChangeEnd = value;
            }
        }

        [Parameter]
        protected Func<Task> ThrottleViewChange
        {
            get => _throttleViewChange;
            set
            {
                if (_throttleViewChange == null && value != null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChange, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChange != null && value == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChange);
                }

                _throttleViewChange = value;
            }
        }


        #endregion Events

        #region Methods

        [JSInvokable]
        public Task EmitMapEvent(MouseEventArgs<BingMapBase> args)
        {
            switch (args.EventName)
            {
                case BingMapEvents.Click:
                    Click?.Invoke(args);
                    break;
                case BingMapEvents.DoubleClick:
                    DoubleClick?.Invoke(args);
                    break;
                case BingMapEvents.RightClick:
                    RightClick?.Invoke(args);
                    break;
                case BingMapEvents.MouseMove:
                    MouseMove?.Invoke(args);
                    break;
                case BingMapEvents.MouseWheel:
                    MouseWheel?.Invoke(args);
                    break;
                case BingMapEvents.MouseDown:
                    MouseDown?.Invoke(args);
                    break;
                case BingMapEvents.MouseOut:
                    MouseOut?.Invoke(args);
                    break;
                case BingMapEvents.MouseOver:
                    MouseOver?.Invoke(args);
                    break;
                case BingMapEvents.MouseUp:
                    MouseUp?.Invoke(args);
                    break;
            }
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task EmitMapChangeEvent(string eventName)
        {
            switch (eventName)
            {
                case BingMapEvents.ViewChangeStart:
                    ViewChangeStart?.Invoke();
                    break;
                case BingMapEvents.ViewChangeEnd:
                    ViewChangeEnd?.Invoke();
                    break;
                case BingMapEvents.ViewChange:
                    ViewChange?.Invoke();
                    break;
                case BingMapEvents.MapTypeChanged:
                    MapTypeChanged?.Invoke();
                    break;
                case "t_" + BingMapEvents.ViewChangeStart:
                    ThrottleViewChangeStart?.Invoke();
                    break;
                case "t_" + BingMapEvents.ViewChangeEnd:
                    ThrottleViewChangeEnd?.Invoke();
                    break;
                case "t_" + BingMapEvents.ViewChange:
                    ThrottleViewChange?.Invoke();
                    break;
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
