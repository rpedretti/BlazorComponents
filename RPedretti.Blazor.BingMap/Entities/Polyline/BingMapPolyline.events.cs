using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.Polyline
{
    public partial class BingMapPolyline : BaseBingMapEntity
    {

        #region Fields

        private const string attachEventFunctionName = _polylineNamespace + ".attachEvent";
        private const string detachEventFunctionName = _polylineNamespace + ".detachEvent";

        #endregion Fields

        #region Events

        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onClick;
        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onDoubleClick;
        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onMouseDown;
        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onMouseOut;
        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onMouseOver;
        private event EventHandler<MouseEventArgs<BingMapPolyline>> _onMouseUp;

        #endregion Events

        #region Methods

        [JSInvokable]
        public Task EmitPolylineEvent(MouseEventArgs<BingMapPolyline> args)
        {
            switch (args.EventName)
            {
                case PolylineEvents.Click:
                    _onClick?.Invoke(this, args);
                    break;
                case PolylineEvents.DoubleClick:
                    _onDoubleClick?.Invoke(this, args);
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnClick
        {
            add
            {
                AssureThisRef();
                if (_onClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.Click, thisRef, nameof(EmitPolylineEvent));
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnDoubleClick
        {
            add
            {
                AssureThisRef();
                if (_onDoubleClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.DoubleClick, thisRef, nameof(EmitPolylineEvent));
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnMouseDown
        {
            add
            {
                AssureThisRef();
                if (_onMouseDown == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseDown, thisRef, nameof(EmitPolylineEvent));
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnMouseOut
        {
            add
            {
                AssureThisRef();
                if (_onMouseOut == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseOut, thisRef, nameof(EmitPolylineEvent));
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnMouseOver
        {
            add
            {
                AssureThisRef();
                if (_onMouseOver == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseOver, thisRef, nameof(EmitPolylineEvent));
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

        public event EventHandler<MouseEventArgs<BingMapPolyline>> OnMouseUp
        {
            add
            {
                AssureThisRef();
                if (_onMouseUp == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolylineEvents.MouseUp, thisRef, nameof(EmitPolylineEvent));
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
