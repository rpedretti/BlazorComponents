using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.Pushpin
{
    public partial class BingMapPushpin : BaseBingMapEntity
    {
        #region Fields

        private const string attachEventFunctionName = _pushpinNamespace + ".attachEvent";
        private const string detachEventFunctionName = _pushpinNamespace + ".detachEvent";

        #endregion Fields

        #region Events

        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onClick;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onDoubleClick;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onDrag;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onDragEnd;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onDragStart;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onMouseDown;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onMouseOut;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onMouseOver;
        private event EventHandler<MouseEventArgs<BingMapPushpin>> _onMouseUp;

        #endregion Events

        #region Methods

        [JSInvokable]
        public Task EmitPushpinEvent(MouseEventArgs<BingMapPushpin> args)
        {
            switch (args.EventName)
            {
                case PolylineEvents.Click:
                    _onClick?.Invoke(this, args);
                    break;
                case PolylineEvents.DoubleClick:
                    _onDoubleClick?.Invoke(this, args);
                    break;
                case PolylineEvents.Drag:
                    _onDrag?.Invoke(this, args);
                    break;
                case PolylineEvents.DragEnd:
                    _onDragEnd?.Invoke(this, args);
                    break;
                case PolylineEvents.DragStart:
                    _onDragStart?.Invoke(this, args);
                    break;
                case PolylineEvents.MouseDown:
                    _onMouseDown?.Invoke(this, args);
                    break;
                case PolylineEvents.MouseOut:
                    _onMouseOut?.Invoke(this, args);
                    break;
                case PolylineEvents.MouseOver:
                    _onMouseOver?.Invoke(this, args);
                    break;
                case PolylineEvents.MouseUp:
                    _onMouseUp?.Invoke(this, args);
                    break;
            }
            return Task.CompletedTask;
        }

        #endregion Methods

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnClick
        {
            add
            {
                AssureThisRef();
                if (_onClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.Click, thisRef, nameof(EmitPushpinEvent));
                }
                _onClick += value;
            }
            remove
            {
                _onClick -= value;
                if (_onClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.Click);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnDoubleClick
        {
            add
            {
                AssureThisRef();
                if (_onDoubleClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.DoubleClick, thisRef, nameof(EmitPushpinEvent));
                }
                _onDoubleClick += value;
            }
            remove
            {
                _onDoubleClick -= value;
                if (_onDoubleClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.DoubleClick);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnDrag
        {
            add
            {
                AssureThisRef();
                if (_onDrag == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.Drag, thisRef, nameof(EmitPushpinEvent));
                }
                _onDrag += value;
            }
            remove
            {
                _onDrag -= value;
                if (_onDrag == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.Drag);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnDragEnd
        {
            add
            {
                AssureThisRef();
                if (_onDragEnd == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.DragEnd, thisRef, nameof(EmitPushpinEvent));
                }
                _onDragEnd += value;
            }
            remove
            {
                _onDragEnd -= value;
                if (_onDragEnd == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.DragEnd);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnDragStart
        {
            add
            {
                AssureThisRef();
                if (_onDragStart == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.DragStart, thisRef, nameof(EmitPushpinEvent));
                }
                _onDragStart += value;
            }
            remove
            {
                _onDragStart -= value;
                if (_onDragStart == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.DragStart);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnMouseDown
        {
            add
            {
                AssureThisRef();
                if (_onMouseDown == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseDown, thisRef, nameof(EmitPushpinEvent));
                }
                _onMouseDown += value;
            }
            remove
            {
                _onMouseDown -= value;
                if (_onMouseDown == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.MouseDown);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnMouseOut
        {
            add
            {
                AssureThisRef();
                if (_onMouseOut == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseOut, thisRef, nameof(EmitPushpinEvent));
                }
                _onMouseOut += value;
            }
            remove
            {
                _onMouseOut -= value;
                if (_onMouseOut == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.MouseOut);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnMouseOver
        {
            add
            {
                AssureThisRef();
                if (_onMouseOver == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseOver, thisRef, nameof(EmitPushpinEvent));
                }
                _onMouseOver += value;
            }
            remove
            {
                _onMouseOver -= value;
                if (_onMouseOver == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.MouseOver);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPushpin>> OnMouseUp
        {
            add
            {
                AssureThisRef();
                if (_onMouseUp == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseUp, thisRef, nameof(EmitPushpinEvent));
                }
                _onMouseUp += value;
            }
            remove
            {
                _onMouseUp -= value;
                if (_onMouseUp == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolylineEvents.MouseUp);
                }
                CheckThisRef();
            }
        }
    }
}
