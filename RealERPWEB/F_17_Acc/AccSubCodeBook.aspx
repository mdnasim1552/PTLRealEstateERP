<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="AccSubCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSubCodeBook" UnobtrusiveValidationMode="None" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            document.getElementById('<%= lnkPageloadData.ClientID %>').click();
        });
        function pageLoaded() {

            $('#Chboxchild').change(function () {
                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };

        function loadModal() {
            $('#detialsinfo').modal('toggle');
        };
        function CloseModal() {
            $('#detialsinfo').modal('hide');
        };
        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };
        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');
        };


        function IsNumberWithOneDecimal(txt, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8)) {
                return false;
            } else {
                var len = txt.value.length;
                var index = txt.value.indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    if ((len + 1) - index > 3) {
                        return false;
                    }
                }
            }
            return true;
        }
        ///Cheeck Mobile
        function checkmobile() { //This function call on text change.   
            var comcod =<%=this.GetComeCode()%>;
            var supmobile = $("#<%=txtSupPhone.ClientID%>")[0].value;
            var url = '<%=ResolveClientUrl("AccSubCodeBook.aspx/CheckPhone")%>';
            console.log(url);
            $.ajax({
                type: "POST",
                url: url,
                //data: '{supmobile: "' + $("#<%=txtSupPhone.ClientID%>")[0].value + '" }',// user name or email value
                data: '{comcod:"' + comcod + '", supmobile:"' + supmobile + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessMobile,
                failure: function (response) {
                    alert(response);
                }
            });
        }

        function checkMobileValidation() {
            var suppmobile = $("#<%=txtSupPhone.ClientID%>")[0].value;
            if (!($.isNumeric(suppmobile))) {
                OnMobileResponse();
                return false;
            } else {
                checkmobile();
            }

        }

        function OnSuccessMobile(response) {
            var msg = $("#<%=lblmobile.ClientID%>")[0];
            switch (response.d) {
                case "true":
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "Mobile/Phone Number Already exist ";
                    $('#<%=lbtnAddCode.ClientID %>').attr("disabled", "disabled");
                    break;
                case "false":
                    msg.style.display = "block";
                    msg.style.color = "green";
                    msg.innerHTML = "Mobile/Phone Number Available";
                    $('#<%=lbtnAddCode.ClientID %>').removeAttr('disabled');
                    break;
            }
        }

        function OnMobileResponse(response2) {
            var msg = $("#<%=lblmobile.ClientID%>")[0];
            msg.style.display = "block";
            msg.style.color = "red";
            msg.innerHTML = "Mobile/Phone Number Must be Numeric";
            $('#<%=lbtnAddCode.ClientID %>').attr("disabled", "disabled");

        }

       <%-- function supMobileCheck() {
            alert("t")
            var supmob = $("#<%=txtSupPhone.ClientID%>")[0].value;
            var supcode = $("#<%=txtresourcecode.ClientID%>")[0].value;
            alert(supcode)
            var ss = supcode.substring(0, 2);
            alert(ss)
            if (ss == "99" || ss == "98") {
                if (supmob.length == 0) {
                    OnSuccessMobile("true");
                } else {
                    CloseModalAddCode();
                }

            }
            else {
                CloseModalAddCode();
            }
            
        }--%>


    </script>


    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <asp:LinkButton ID="lnkPageloadData" Style="display: none" OnClick="lnkPageloadData_Click" Class="btn btn-sm btn-primary d-none" runat="server">lnkPageloadData</asp:LinkButton>
            <div class="card card-fluid mt-4">
                <div class="card-header">
                    <div class="row">
                        <button class="btn btn-sm btn-secsondary col-1" type="button">Code Book</button>
                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="chzn-select col-2" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <button class="btn btn-sm btn-secsondary mr-1 col-1" type="button">Level</button>
                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="chzn-select col-2" runat="server">
                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                        </asp:DropDownList>
                        <button class="btn btn-sm btn-secsondary col-1" type="button">Catagory</button>
                        <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="chzn-select col-3">
                        </asp:DropDownList>
                    </div>
                    <div class="row mt-2">
                        <div class="col-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <asp:Label ID="LblBookName2" runat="server" CssClass="btn btn-secondary btn-sm" Text="Search Option:"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-secondary btn-sm">  <i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-sm btn-primary "></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <asp:Label ID="lblPage" runat="server" CssClass="btn btn-secondary btn-sm" Text="Page Size"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem Selected="True">900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="row mb-2">
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="900" OnPageIndexChanging="grvacc_PageIndexChanging"
                                OnRowDataBound="grvacc_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                                Width="700px" OnDataBound="grvacc_DataBound" ShowFooter="True">
                                <FooterStyle BackColor="#5F9467" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                            <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" HeaderStyle-Width="50px"
                                        SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">

                                        <ItemTemplate>
                                            <asp:Label ID="lbllgrcode" runat="server" Font-Size="12px"
                                                MaxLength="13" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code" HeaderStyle-Width="400px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="250"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="350px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="350px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                Width="350px">    
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code BN">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDescbn" runat="server" Font-Size="12px" MaxLength="250"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescbn")) %>'
                                                Width="280px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code BN" Width="280px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescbn"))  %>' Width="280px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirunit" runat="server" MaxLength="100" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="80px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlUnit" CssClass="chzn-select form-control" Visible="false" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std.Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsirval" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="10"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand" HeaderStyle-Width="200">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="200px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <EditItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>

                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProName" runat="server" CssClass=" ddlPage" Style="width: 200px;" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProNames" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetails" runat="server" CssClass="btn btn-sm  btn-primary" Visible="false" OnClick="lbtnDetails_Click">Details</asp:LinkButton>

                                            <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="tlblgvUsr" runat="server" Font-Size="12px" MaxLength="100"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                Width="90px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sir Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllgrcodefull" runat="server" Font-Size="12px"
                                                MaxLength="13" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seq" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvseq" runat="server"
                                                Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "seq")) %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnDeptSeqUpdate" runat="server" CssClass="btn btn-success btn-sm successBtn" OnClick="lnkbtnDeptSeqUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mapping" Visible="false">
                                         <EditItemTemplate>
                                              <asp:Label ID="lblgvMapDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mapdesc")) %>'
                                                Width="80px"></asp:Label>
                                           <asp:DropDownList ID="ddlMapping" runat="server" CssClass="form-control form-control-sm chzn-select" Width="80px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMapDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mapdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade " id="detialsinfo" role="dialog">
                <div class="modal-dialog  modal-lg ">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 class="modal-title text-center">Details Information</h4>
                            <button type="button" data-dismiss="modal" class="btn btn-xs btn-danger" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label class="control-label">Details:</label>
                                <asp:TextBox ID="txtrsircode" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>

                            </div>

                            <%-- <div class="form-group">
                                    <label class="control-label">Catagory:</label>
                                  
                                    <asp:DropDownList ID="ddlworkCatagory" runat="server" CssClass="form-control">

                                        <asp:ListItem Value="">Standard</asp:ListItem>
                                        <asp:ListItem Value="A">Appartment</asp:ListItem>
                                        <asp:ListItem Value="O">Office</asp:ListItem>

                                    </asp:DropDownList>

                                </div>--%>
                        </div>
                        <div class="modal-footer">
                            <%--    <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" >Update</button>--%>
                            <asp:LinkButton ID="lbtnUpdateDetails" runat="server" class="btn btn-success" aria-hidden="true" OnClientClick="CloseModal();" OnClick="lbtnUpdateDetails_Click">Update</asp:LinkButton>
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New Code</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Resource Code </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtresourcecode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row mb-1">

                                <label class="col-md-4">Description EN</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtresourcehead" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description BN</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtresourceheadBN" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1" runat="server" id="divMobile">
                                <label class="col-md-4">Phone Number</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtSupPhone" TextMode="Number" MaxLength="12" min="0" placeholder="Supplier Phone" runat="server" CssClass="form-control" onchange="checkMobileValidation()" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorNum" runat="server" Display="Dynamic" ErrorMessage="Accepts only numbers with digit 8 to 12" ValidationGroup="RegisterCheck" ForeColor="Red" ControlToValidate="txtSupPhone" ValidationExpression="[0-9]{12}">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <label class="col-md-4">Unit </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtunit" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                    <asp:DropDownList ID="ddlUnits" CssClass="chzn-select form-control" Visible="false" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <label id="chkbod" runat="server" class="switch">
                                        <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                        <span class="btn btn-xs slider round"></span>
                                    </label>
                                    <%--<asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                </div>

                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4" id="lblsdrate" runat="server">Standard Rate </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Brand </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Details</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtTDetails" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label id="lblddlproject" runat="server" class="col-md-4">Department</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label id="lblMapping" runat="server" class="col-md-4">Mapping</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlModMapping" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click" ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


