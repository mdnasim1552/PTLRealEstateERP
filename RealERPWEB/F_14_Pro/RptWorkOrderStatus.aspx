<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptWorkOrderStatus" %>

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

            var gvDeWorkOrdSt = $('#<%=this.gvDeWorkOrdSt.ClientID%>');


            
            gvDeWorkOrdSt.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 3


            });

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
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control  inputTxt" style="width:336px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="margin-left:-50px" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt smLbl_to" Style="margin-right: 7px;">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#DBEBD4"
                                            CssClass="rbtnList1"
                                            OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6"
                                            RepeatDirection="Horizontal" Width="300px">
                                            <asp:ListItem>Requisition Basis</asp:ListItem>
                                            <asp:ListItem>Material Basis</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbtnpurtype" runat="server" Visible="true" BackColor="#DBEBD4" CssClass="rbtnList1" RepeatColumns="6" RepeatDirection="Horizontal" Width="300px">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Credit</asp:ListItem>
                                            <asp:ListItem  Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="ChkBalance" runat="server" Text="Without Zero Balance" CssClass="chkBoxControl margin5px" />
                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="WorkIOrdStatus" runat="server">
                           <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvReqStatus_PageIndexChanging"
                                            Style="text-align: left" Width="889px">
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
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Req. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReqNo1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRF No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMrfNo" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDate" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
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
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                            Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Req. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Completed">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrderQty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remaining Order">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                                            Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpfDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                        </asp:View>
                        <asp:View ID="DetailsWorkIOrdStatus" runat="server">
                        
                          
                                <asp:GridView ID="gvDeWorkOrdSt" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="831px" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea "
                                    OnPageIndexChanging="gvDeWorkOrdSt_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPactdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdRef" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdDat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno2")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appoved No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAppNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno2")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Suppliers Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMatDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBrName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                   Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOrderqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approve Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAppqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrAppUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprusername")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Print">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrPrUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orusername")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />


                                </asp:GridView>
                                     
                         
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>




            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="11">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">

                                <tr>
                                    <td class="style29">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 1018px;">
                                                <tr>
                                                    <td class="style35" style="text-align: left">
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style36" style="text-align: left">
                                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat"
                                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td class="style44" style="text-align: left">
                                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px"
                                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnFindProject_Click"
                                                            Width="16px" />
                                                    </td>
                                                    <td class="style67" style="text-align: left" colspan="3">
                                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="300px">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                                        </cc1:ListSearchExtender>

                                                        <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#99FFCC" Height="20px"
                                                            OnClick="lnkbtnOk_Click" Style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: left" class="style58"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                </tr>
                                                <tr>
                                                    <td class="style46" style="text-align: left">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="color: #FFFFFF; text-align: right;" Text="Date:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style47" style="text-align: left">
                                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style48" style="text-align: left">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="text-align: right; color: #FFFFFF;" Text="To:"></asp:Label>
                                                    </td>
                                                    <td class="style49" style="text-align: left" colspan="3">
                                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat"
                                                            Font-Bold="False" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style57" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                </tr>
                                                <tr>
                                                    <td class="style35" style="text-align: left">
                                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="#993300" Style="color: #FFFFFF; text-align: right;" Text="Page Size"
                                                            Visible="False" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style36" style="text-align: left">
                                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                            <asp:ListItem>100</asp:ListItem>
                                                            <asp:ListItem>150</asp:ListItem>
                                                            <asp:ListItem>200</asp:ListItem>
                                                            <asp:ListItem>300</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style44" style="text-align: left">&nbsp;</td>
                                                    <td class="style68" style="text-align: left">
                                                       
                                                    </td>
                                                    <td class="style62" style="text-align: left">
                                                        
                                                    </td>
                                                    <td class="style62" style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left" class="style58">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



