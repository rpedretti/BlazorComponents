using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.Polygon
{
    public partial class BingMapPolygon : BaseBingMapEntity
    {
        #region Fields

        private const string attachEventFunctionName = _polygonNamespace + ".attachEvent";
        private const string detachEventFunctionName = _polygonNamespace + ".detachEvent";

        #endregion Fields

        #region Events

        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onClick;
        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onDoubleClick;
        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onMouseDown;
        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onMouseOut;
        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onMouseOver;
        private event EventHandler<MouseEventArgs<BingMapPolygon>> _onMouseUp;

        #endregion Events

        #region Methods

        [JSInvokable]
        public Task EmitPolygonEvent(MouseEventArgs<BingMapPolygon> args)
        {
            switch (args.EventName)
            {
                case PolygonEvents.Click:
                    _onClick?.Invoke(this, args);
                    break;
                case PolygonEvents.DoubleClick:
                    _onDoubleClick?.Invoke(this, args);
                    break;
                case PolygonEvents.MouseDown:
                    _onMouseDown?.Invoke(this, args);
                    break;
                case PolygonEvents.MouseOut:
                    _onMouseOut?.Invoke(this, args);
                    break;
                case PolygonEvents.MouseOver:
                    _onMouseOver?.Invoke(this, args);
                    break;
                case PolygonEvents.MouseUp:
                    _onMouseUp?.Invoke(this, args);
                    break;
            }
            return Task.CompletedTask;
        }

        #endregion Methods

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnClick
        {
            add
            {
                AssureThisRef();
                if (_onClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.Click, thisRef, nameof(EmitPolygonEvent));
                }
                _onClick += value;
            }
            remove
            {
                _onClick -= value;
                if (_onClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.Click);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnDoubleClick
        {
            add
            {
                AssureThisRef();
                if (_onDoubleClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.DoubleClick, thisRef, nameof(EmitPolygonEvent));
                }
                _onDoubleClick += value;
            }
            remove
            {
                _onDoubleClick -= value;
                if (_onDoubleClick == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.DoubleClick);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnMouseDown
        {
            add
            {
                AssureThisRef();
                if (_onMouseDown == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.MouseDown, thisRef, nameof(EmitPolygonEvent));
                }
                _onMouseDown += value;
            }
            remove
            {
                _onMouseDown -= value;
                if (_onMouseDown == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.MouseDown);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnMouseOut
        {
            add
            {
                AssureThisRef();
                if (_onMouseOut == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.MouseOut, thisRef, nameof(EmitPolygonEvent));
                }
                _onMouseOut += value;
            }
            remove
            {
                _onMouseOut -= value;
                if (_onMouseOut == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.MouseOut);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnMouseOver
        {
            add
            {
                AssureThisRef();
                if (_onMouseOver == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.MouseOver, thisRef, nameof(EmitPolygonEvent));
                }
                _onMouseOver += value;
            }
            remove
            {
                _onMouseOver -= value;
                if (_onMouseOver == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.MouseOver);
                }
                CheckThisRef();
            }
        }

        public event EventHandler<MouseEventArgs<BingMapPolygon>> OnMouseUp
        {
            add
            {
                AssureThisRef();
                if (_onMouseUp == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(attachEventFunctionName, Id, PolygonEvents.MouseUp, thisRef, nameof(EmitPolygonEvent));
                }
                _onMouseUp += value;
            }
            remove
            {
                _onMouseUp -= value;
                if (_onMouseUp == null)
                {
                    JSRuntime.Current.InvokeAsync<object>(detachEventFunctionName, Id, PolygonEvents.MouseUp);
                }
                CheckThisRef();
            }
        }
    }
}
