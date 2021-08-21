<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="InterCompMatTranStatusDel.aspx.cs" Inherits="RealERPWEB.F_17_Acc.InterCompMatTranStatusDel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <script language="javascript" type="text/javascript">

                $(document).ready(function () {
                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

                });
                function pageLoaded() {

                    $("input, select").bind("keydown", function (event) {
                        var k1 = new KeyPress();
                        k1.textBoxHandler(event);
                    });

                    $('.chzn-select').chosen({ search_contains: true });
                }
            </script>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">From Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">To</asp:Label>

                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-7 pading5px asitCol7">

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl">Ref. No.:</asp:Label>
                                        <asp:TextBox ID="txtSrcRefNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindRefno" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindRefno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                       
                                        <%-- <div class="col-md-3 pading5px">
                                            <asp:CheckBox ID="ChkComTranfrm" runat="server" Text="Company From" CssClass="btn btn-primary checkBox" />

                                        </div>

                                         <div class="col-md-2 pading5px">
                                            <asp:CheckBox ID="ChkPrjTransFrm" runat="server" Text="Proj From" CssClass="btn btn-primary checkBox" />

                                        </div>
                                        <div class="col-md-2 pading5px ">
                                            <asp:CheckBox ID="ChkComTransTo" runat="server" Text="Company To " CssClass="btn btn-primary checkBox" />

                                        </div>

                                        <div class="col-md-2 pading5px ">
                                            <asp:CheckBox ID="chkProjectTrnsTo" runat="server" Text="Proj To" CssClass="btn btn-primary checkBox" />

                                        </div>--%>
                                    </div>
                                      <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="grvacc_PageIndexChanging"
                        ShowFooter="True" Width="501px" OnRowDeleting="grvacc_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Voucher #">
                                <ItemTemplate>
                                    <asp:Label ID="lblvouNo" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Voucher Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblvoudat" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ref. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblrefno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="Company From">
                                <ItemTemplate>
                                    <asp:Label ID="lbComFrom" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ftcomdesc"))  %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Compnay To">
                                <ItemTemplate>
                                    <asp:Label ID="lblComTo" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttcomdesc"))  %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project From">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjfrm" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfprjdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project To">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjTo" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttprjdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>

                              <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:Label ID="lblMat" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Qty.">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="55px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbltotalt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Text="Total" Width="55px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="85px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="85px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
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






            

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

