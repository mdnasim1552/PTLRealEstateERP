<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ConBillTracking.aspx.cs" Inherits="RealERPWEB.F_09_PImp.ConBillTracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            var gv = $('#<%=this.gvCBillTracking.ClientID %>');
            gv.Scrollable();
        }
    </script>




    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label12" runat="server" CssClass="smLbl_to"
                                            Text="Field Information:"></asp:Label>
                                        <asp:CheckBox ID="chkallCBillTracking" runat="server" AutoPostBack="True" CssClass="btn btn-primary primaryBtn checkBox" OnCheckedChanged="chkallCBillTracking_CheckedChanged" Text="Check All" />
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblfdate" runat="server" CssClass=" smLbl_to"> Form</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                          <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>

                                        <asp:Label ID="tDate" runat="server" CssClass=" smLbl_to"> To </asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                       <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class=" form-group">

                                    <asp:CheckBoxList ID="cblCBillTracking" runat="server" CellPadding="2" CellSpacing="0"
                                        CssClass=" rbtnList1"
                                        RepeatColumns="12" Width="1067px"
                                        OnSelectedIndexChanged="cblCBillTracking_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                        <asp:ListItem>dd</asp:ListItem>
                                        <asp:ListItem>ee</asp:ListItem>
                                        <asp:ListItem>ff</asp:ListItem>
                                        <asp:ListItem>gg</asp:ListItem>
                                        <asp:ListItem>hh</asp:ListItem>
                                        <asp:ListItem>mm</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="Panel4" runat="server">

                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">

                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"
                                                Text="Search List"></asp:Label>

                                            <asp:DropDownList ID="ddlFieldList1" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                            </asp:DropDownList>

                                            <div class="clearfix"></div>
                                            <div class="form-group">

                                                 <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName"
                                                Text=""></asp:Label>
                                                
                                                 <asp:DropDownList ID="ddlFieldList2" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                                          </asp:DropDownList>

                                               
                                            </div>
                                            <div class="form-group">
                                                  <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName"
                                                Text=""></asp:Label>
                                                <asp:DropDownList ID="ddlFieldList3" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                                </asp:DropDownList>

                                                
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                                    Text="Page size:" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage62" Visible="False" Width="152px">
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                        </div>

                                        <div class="col-md-1 pading5px">
                                            <div>
                                                <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="form-group">
                                                
                                                <asp:DropDownList ID="ddlSrch2" runat="server" Width="90px" CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px" CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>


                                         <div class="col-md-2 pading5px asitCol2">
                                                <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:DropDownList ID="ddlOperator1" runat="server" CssClass=" ddlPage62 inputTxt">
                                                    <asp:ListItem Value="and">And</asp:ListItem>
                                                    <asp:ListItem Value="or">Or</asp:ListItem>
                                                </asp:DropDownList>
                                             <div class="clearfix"></div>
                                                <asp:TextBox ID="txtSearch2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                             <asp:DropDownList ID="ddlOperator2" runat="server"
                                                    CssClass="ddlPage62 inputTxt">
                                                    <asp:ListItem Value="and">And</asp:ListItem>
                                                    <asp:ListItem Value="or">Or</asp:ListItem>
                                                </asp:DropDownList>
                                           
                                             <div class="clearfix"></div>

                                             <asp:TextBox ID="txtSearch3" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                              
                                            </div>

                                        <div class="col-md-5 pading5px">
                                           

                                            <asp:Panel ID="Panel5" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to"
                                                                Text="Order Field:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>

                                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <asp:Label ID="Label3" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td class="style115">
                                                            <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style116">
                                                            <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>


                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>

                                      
                                    </div>

                                </div>
                            </fieldset>


                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel10" runat="server">
                            <asp:GridView ID="gvCBillTracking" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="616px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvCBillTracking_RowDataBound" AllowPaging="True"
                                OnPageIndexChanging="gvCBillTracking_PageIndexChanging">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText=" Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBillNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMRDate" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Bill Ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBillRef" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvProDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material's Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSpc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcdesc")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvQty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRate" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Suplier's Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc9" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvVNum" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

