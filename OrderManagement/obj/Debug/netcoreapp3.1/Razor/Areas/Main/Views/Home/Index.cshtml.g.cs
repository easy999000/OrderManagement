#pragma checksum "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e6a948e208d752643ca799d4a64ec49ca82d307"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Main_Views_Home_Index), @"mvc.1.0.view", @"/Areas/Main/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e6a948e208d752643ca799d4a64ec49ca82d307", @"/Areas/Main/Views/Home/Index.cshtml")]
    public class Areas_Main_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e6a948e208d752643ca799d4a64ec49ca82d3072986", async() => {
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>Index</title>\r\n    <style>\r\n        div {\r\n        margin-top:2px;\r\n        margin-bottom:2px;\r\n        padding-top:2px;\r\n        padding-bottom:2px;\r\n        }\r\n    </style>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e6a948e208d752643ca799d4a64ec49ca82d3074222", async() => {
                WriteLiteral("\r\n    <div>\r\n        <div>\r\n            用户列表: <a target=\"_blank\"");
                BeginWriteAttribute("href", " href=\"", 391, "\"", 428, 1);
#nullable restore
#line 24 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 398, Url.Action("index","Account"), 398, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">用户列表 </a>\r\n            <div>\r\n                添加用户: <div>\r\n                    <form target=\"_blank\" method=\"post\"");
                BeginWriteAttribute("action", " action=\"", 544, "\"", 612, 1);
#nullable restore
#line 27 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 553, Url.Action("AddAccount","account",new {area="authority" }), 553, 59, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                        
                        <label>Account:<input type=""text"" name=""Account"" /></label>
                        <label>Pass:<input type=""text"" name=""Pass"" /></label>
                        <label>Name:<input type=""text"" name=""Name"" /></label>
                        <input type=""submit"" value=""添加"" />
                    </form>
                </div>
            </div>
        </div>
        <div style=""height:1px;width:100%;background-color:forestgreen""></div>
        <div>
            角色列表: <a target=""_blank""");
                BeginWriteAttribute("href", " href=\"", 1165, "\"", 1199, 1);
#nullable restore
#line 39 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 1172, Url.Action("index","Role"), 1172, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">角色列表</a>\r\n            <div>\r\n                添加角色: <div>\r\n                    <form target=\"_blank\" method=\"post\"");
                BeginWriteAttribute("action", " action=\"", 1314, "\"", 1376, 1);
#nullable restore
#line 42 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 1323, Url.Action("AddRole","Role",new {area="authority" }), 1323, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                        <label>RoleName:<input type=""text"" name=""RoleName"" /></label>
                        <label>Description:<input type=""text"" name=""Description"" /></label>
                        <input type=""submit"" value=""添加"" />
                    </form>
                </div>
            </div>
        </div>
        <div style=""height:1px;width:100%;background-color:forestgreen""></div>
        <div>
            基础权限: <a target=""_blank""");
                BeginWriteAttribute("href", " href=\"", 1840, "\"", 1884, 1);
#nullable restore
#line 52 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 1847, Url.Action("index","PermissionBase"), 1847, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">基础权限</a>\r\n");
                WriteLiteral("        </div>\r\n        <div style=\"height:1px;width:100%;background-color:forestgreen\"></div>\r\n        <div>\r\n            用户关联角色: <div>\r\n                <form target=\"_blank\" method=\"post\"");
                BeginWriteAttribute("action", " action=\"", 2559, "\"", 2628, 1);
#nullable restore
#line 66 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 2568, Url.Action("RelatedRole","account",new {area="authority" }), 2568, 60, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                    <label>用户id:<input type=""text"" name=""AccountId"" /></label>
                    <label>角色id:<input type=""text"" name=""RoleId"" /></label>
                    <input type=""submit"" value=""添加"" />
                </form>
            </div>
        </div>
        <div style=""height:1px;width:100%;background-color:forestgreen""></div>
        <div>
            角色关联权限: <div>
                <form target=""_blank"" method=""post""");
                BeginWriteAttribute("action", " action=\"", 3079, "\"", 3148, 1);
#nullable restore
#line 76 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 3088, Url.Action("RelatedPerBase","Role",new {area="authority" }), 3088, 60, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                    <label>权限id:<input type=""text"" name=""BaseId"" /></label>
                    <label>角色id:<input type=""text"" name=""RoleId"" /></label>
                    <input type=""submit"" value=""添加"" />
                </form>
            </div>
        </div>
        <div style=""height:1px;width:100%;background-color:forestgreen""></div>
        <div>
            权限测试页面: <div>
                <a target=""_blank""");
                BeginWriteAttribute("href", " href=\"", 3579, "\"", 3630, 1);
#nullable restore
#line 86 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 3586, Url.Action("a1","test1",new {area="test" }), 3586, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">a1 </a><br />\r\n                <a target=\"_blank\"");
                BeginWriteAttribute("href", " href=\"", 3681, "\"", 3732, 1);
#nullable restore
#line 87 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 3688, Url.Action("a2","test1",new {area="test" }), 3688, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">a2 </a><br />\r\n                <a target=\"_blank\"");
                BeginWriteAttribute("href", " href=\"", 3783, "\"", 3834, 1);
#nullable restore
#line 88 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 3790, Url.Action("a3","test1",new {area="test" }), 3790, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">a3 </a><br />\r\n                <a target=\"_blank\"");
                BeginWriteAttribute("href", " href=\"", 3885, "\"", 3936, 1);
#nullable restore
#line 89 "D:\zhaopeng\code\OrderManagement\OrderManagement\Areas\Main\Views\Home\Index.cshtml"
WriteAttributeValue("", 3892, Url.Action("a4","test1",new {area="test" }), 3892, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">a4 </a><br />\r\n            </div>\r\n        </div>\r\n        <div style=\"height:1px;width:100%;background-color:forestgreen\"></div>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
