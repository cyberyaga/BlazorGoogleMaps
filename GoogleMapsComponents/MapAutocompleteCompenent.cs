using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GoogleMapsComponents.Maps.Places;

namespace GoogleMapsComponents
{
    public class MapAutocompleteComponent : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime JsRuntime { get; protected set; }

        public Autocomplete InteropObject { get; private set; }

        public async Task InitAsync(ElementReference element, AutocompleteOptions options = null)
        {
            InteropObject = await Autocomplete.CreateAsync(JsRuntime, element, options);
        }

        public void Dispose()
        {
            InteropObject?.Dispose();
        }
    }
}
