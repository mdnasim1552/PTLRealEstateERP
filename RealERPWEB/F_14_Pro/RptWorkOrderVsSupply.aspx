<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderVsSupply.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptWorkOrderVsSupply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>



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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblAcchead" runat="server" CssClass="lblTxt lblName" Text="Supplier Name"></asp:Label>
                                            <asp:TextBox ID="txtSrcSupplier" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" style="width:336px" CssClass="chzn-select form-control  inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        
                                        <div class="col-md2 pading5px ">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" style="width:336px" CssClass="chzn-select  ddlPage125 ">
                                            </asp:DropDownList>

                                        </div>

                                       </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblmatrial" runat="server" CssClass="lblTxt lblName" Text="Material Name"></asp:Label>
                                            <asp:TextBox ID="txtmatreial" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="btnMaterial" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="btnMaterial_Click" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                          
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlMaterial" runat="server" style="width:336px" CssClass="chzn-select  ddlPage125 ">
                                            </asp:DropDownList>

                                           

                                        </div>


                                        <div class="col-md-1">
                                         <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkbtnOk" runat="server"  Style=" margin-left:-65px;"  CssClass="btn btn-primary primaryBtn"  OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                            </div>
                                        
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="lbldateRange" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                            <div class="smLbl_to">

                                                <asp:Label ID="lblTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                            </div>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:CheckBox ID="ChkBalance" runat="server" CssClass="btn btn-primary chkBoxControl primaryBtn " Style="margin-left:-9px" Text="Without Zero Balance" />
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" Visible="false" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass="ddlPage">
                                                <asp:ListItem Value="10">10</asp:ListItem>
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

                                        <div class="clearfix"></div>
                                    </div>


                                </fieldset>
                            </div>
                            <div class="table-responsive row">
                                <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvReqStatus_PageIndexChanging"
                                    Width="889px" ShowFooter="True">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupplier" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Order. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>


                        </asp:View>
                        <asp:View ID="View2" runat="server">

                            <asp:Panel ID="Panel3" runat="server">

                                <fieldset class="scheduler-border fieldset_A">
                                    <legend>
                                        <asp:Label ID="Label9" runat="server" Text="Order Tracking"></asp:Label>
                                    </legend>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Order No"></asp:Label>
                                            <asp:TextBox ID="txtSrcOrder" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnFindOrder" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindOrder_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="chzn-select form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOrderTk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOrderTk_Click">Ok</asp:LinkButton>

                                        </div>

                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtorddate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtorddate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtorddate"></cc1:CalendarExtender>
                                            <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender3" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                                            </cc1:ListSearchExtender>

                                        </div>

                                        <div class="clearfix"></div>
                                    </div>


                                </fieldset>


                            </asp:Panel>
                            <div class="row table-responsive">
                                <asp:GridView ID="gvOrdertk" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="982px" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDescor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDescord" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnitord" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpecification" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQtyor" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrrdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRR Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrrqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbillno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrrdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>





        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>



