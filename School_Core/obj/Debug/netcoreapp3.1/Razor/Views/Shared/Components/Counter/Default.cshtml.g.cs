#pragma checksum "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03db7e26b9e0fd1f0be17d8b4301c6f332e804bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Counter_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Counter/Default.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\_ViewImports.cshtml"
using School_Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\_ViewImports.cshtml"
using School_Core.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03db7e26b9e0fd1f0be17d8b4301c6f332e804bc", @"/Views/Shared/Components/Counter/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d16b3db5fa21aae20172dc41a5208c2bb5ca2bb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Counter_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<School_Core.ViewModels.CounterTableViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col\">\r\n        <h4>");
#nullable restore
#line 5 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
       Write(Model.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n\r\n        <table");
            BeginWriteAttribute("style", " style=\"", 148, "\"", 192, 4);
            WriteAttributeValue("", 156, "border:", 156, 7, true);
            WriteAttributeValue(" ", 163, "2px", 164, 4, true);
            WriteAttributeValue(" ", 167, "solid", 168, 6, true);
#nullable restore
#line 7 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
WriteAttributeValue(" ", 173, Model.BorderColor, 174, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n            <tbody>\r\n");
#nullable restore
#line 10 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
                 foreach (var item in Model.CounterViewModels)
                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 14 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
                       Write(item.RowName);

#line default
#line hidden
#nullable disable
            WriteLiteral(":</td>\r\n                        <td>");
#nullable restore
#line 15 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
                       Write(item.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 17 "C:\Users\KarmoKiima\source\repos\School_Core\School_Core\Views\Shared\Components\Counter\Default.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<School_Core.ViewModels.CounterTableViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
