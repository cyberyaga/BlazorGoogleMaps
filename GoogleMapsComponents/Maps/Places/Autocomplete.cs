using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GoogleMapsComponents.Maps.Places
{
    public class Autocomplete : IDisposable, IJsObjectRef
    {
        private readonly JsObjectRef _jsObjectRef;

        public Guid Guid => _jsObjectRef.Guid;

        public Dictionary<ControlPosition, List<ElementReference>> Controls { get; private set; }

        public MapData Data { get; private set; }

        public static async Task<Autocomplete> CreateAsync(
            IJSRuntime jsRuntime,
            ElementReference autocompDiv,
            AutocompleteOptions opts = null)
        {
            var jsObjectRef = await JsObjectRef.CreateAsync(jsRuntime, "google.maps.places.Autocomplete", autocompDiv, opts);
            var dataObjectRef = await jsObjectRef.GetObjectReference("data");
            var data = new MapData(dataObjectRef);
            var autoComp = new Autocomplete(jsObjectRef, data);

            JsObjectRefInstances.Add(autoComp);

            return autoComp;
        }

        private Autocomplete(JsObjectRef jsObjectRef, MapData data)
        {
            _jsObjectRef = jsObjectRef;
            Data = data;
        }

        public void Dispose()
        {
            JsObjectRefInstances.Remove(_jsObjectRef.Guid.ToString());
            _jsObjectRef.Dispose();
        }

        public async Task<LatLngBounds> GetBounds()
        {
            return await _jsObjectRef.InvokeAsync<LatLngBounds>("getPosition");
        }

        public async Task<string[]> GetFields()
        {
            return await _jsObjectRef.InvokeAsync<string[]>("getFields");
        }

        public async Task<PlaceResult> GetPlace()
        {
            return await _jsObjectRef.InvokeAsync<PlaceResult>("getPlace");           
        }

        public async Task SetBounds(LatLngBounds bounds)
        {
            await _jsObjectRef.InvokeAsync("setBounds", bounds);
        }

        public async Task SetComponentRestrictions(ComponentRestrictions restrictions)
        {
            await _jsObjectRef.InvokeAsync("setComponentRestrictions", restrictions);
        }

        public async Task SetFields(string[] fields)
        {
            await _jsObjectRef.InvokeAsync("setFields", fields);
        }

        public async Task SetOptions(AutocompleteOptions options)
        {
            await _jsObjectRef.InvokeAsync("setOptions", options);
        }

        public async Task SetTypes(string[] types)
        {
            await _jsObjectRef.InvokeAsync("setTypes", types);
        }

        public async Task<MapEventListener> AddListener(string eventName, Action handler)
        {
            var listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync(
                "addListener", eventName, handler);

            return new MapEventListener(listenerRef);
        }

        public async Task<MapEventListener> AddListener<T>(string eventName, Action<T> handler)
        {
            var listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync(
                "addListener", eventName, handler);

            return new MapEventListener(listenerRef);
        }
    }
}
