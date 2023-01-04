<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPurchaseStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            <%-- var gv1 = $('#<%=this.gvPurStatus.ClientID %>');
            gv1.Scrollable();
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);--%>

            var gvPurStatus = $('#<%=this.gvPurStatus.ClientID%>');
            var gvPurMatRVar = $('#<%=this.gvPurMatRVar.ClientID%>');

            gvPurStatus.gridviewScroll({
                width: 1160,
                height: 420,
                barsize: 8,
                startHorizontal: 3,
                wheelstep: 10,
                arrowsize: 30,
                railsize: 16,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });

            gvPurMatRVar.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });
            $('.chzn-select').chosen({ search_contains: true });
        };
        function printTracking(data) {
            window.open(data, '_blank');
        }

    </script>

    <style>
        .grvContentarea > thead > tr > th, .grvContentarea > tbody > tr > th, .grvContentarea > tfoot > tr > th {
            padding: 5px 4px;
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                
                                <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6" id="main" runat="server">
                                        <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcProject" Style="display: none" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_OnSelectedIndexChanged" AutoPostBack="True" runat="server"  CssClass="chzn-select  ddlPage">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-5 pading5px asitCol6" runat="server" id="serail" visible="False">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Serial Number"></asp:Label>
                                        <asp:TextBox ID="TextBox1" Style="display: none" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlSerialno" AutoPostBack="True" runat="server" Style="width: 322px" CssClass="chzn-select  ddlPage">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-5 pading5px asitCol6" runat="server" id="genbillno" visible="False">
                                       <asp:Label ID="lblGenBillTrack" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="TextGenBillTrack"  runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="LinkGenBillTrack" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="LinkGenBillTrack_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlGenBillTracking"  AutoPostBack="True" runat="server" Style="width: 250px" CssClass="chzn-select  ddlPage">
                                        </asp:DropDownList>
                                    </div>


                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to" Visible="false">Group</asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage" Visible="False" Width="80px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Visible="false">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage" Visible="false" Style="width: 72px;">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1200</asp:ListItem>
                                            <asp:ListItem>1500</asp:ListItem>
                                            <asp:ListItem>3000</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="col-md-12 pading5px asitCol11">

                                        <div runat="server" id="datepart" class="col-md-6 padding5px">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                             <asp:Label ID="lblMcod" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Material"></asp:Label>

                                            <asp:DropDownList ID="ddlMatCode" runat="server" Visible="false" Width="150px" CssClass="chzn-select ddlPage">
                                            </asp:DropDownList>

                                        </div>

                                      
                                        
                                           
                                    

                                        <div class="col-md-4 pull-right">
                                            <asp:CheckBox ID="chkDirect" runat="server" Visible="false" Text="Petty Cash" CssClass="btn btn-primary checkBox" />

                                            <asp:Label ID="LblReqno" runat="server" CssClass="lblTxt lblName" Visible="false" Text="MRR REF"></asp:Label>
                                            <asp:TextBox ID="txtSrcMrfNo" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>

                                            <div class="col-md-1">
                                                <asp:LinkButton ID="imgbtnFindRequiSition" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindRequiSition_Click" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>
                                        </div>


                                    </div>
                                </div>

                            </div>

                        </fieldset>


                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="vdaywisepurchase" runat="server">
                                <asp:Panel ID="PnlSupplier" runat="server" Visible="False">
                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px ">
                                                    <asp:Label ID="lblsupplier" runat="server" CssClass="lblTxt lblName" Text="Supplier"></asp:Label>
                                                  
                                                   <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control  chzn-select" style="width:380px">
                                                    </asp:DropDownList>

                                                </div>

                                               
                                            </div>

                                        </div>
                                    </fieldset>
                                </asp:Panel>


                                <asp:GridView ID="gvPurStatus" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPurStatus_PageIndexChanging" ShowFooter="True" Width="831px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Desc." Width="110px"></asp:Label>

                                                <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Chalan No ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchalano" Style="word-break: break-all" runat="server" Width="120px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chalanno"))  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRR Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="72px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Chalan No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgchlno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBillNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Bill Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="135px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFUnit" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px">Total : </asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecifi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="bill Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbillqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbillAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrsumpname" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>


                            </asp:View>

                            <asp:View ID="ViewResSummary" runat="server">
                                <asp:GridView ID="gvPurSum" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvPurSum_PageIndexChanging" ShowFooter="True" Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc0" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtS" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </asp:View>


                            <asp:View ID="Pendingbill" runat="server">
                            </asp:View>


                            <asp:View ID="Purchasetrk" runat="server">


                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Requisition"></asp:Label>
                                                <asp:TextBox ID="txtSrcRequisition01" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgbtnFindReqno01" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindReqno01_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>

                                            </div>

                                            <div class="col-md-3 pading5px  asitCol3">
                                                <asp:DropDownList ID="ddlReqNo01" runat="server" CssClass="chzn-select form-control inputTxt" Style="width: 336px;">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 pading5px  asitCol1">
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>


                                
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea" OnRowDataBound="gvPurstk01_RowDataBound">

                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Chalan No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgchlno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="170px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRemark" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqnote")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Print">
                                                <ItemTemplate>                                                    
                                                    <asp:LinkButton ID="btnPrintReqInfo" OnClick="btnPrintReqInfo_Click" runat="server" CssClass="btn btn-default btn-xs" Style="background-color:#C0C0C0"><span style="color:#002bff" class="fa fa-print"></span> </asp:LinkButton>

                                                </ItemTemplate>

                                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>


                                <asp:Panel ID="pnlnarration" runat="server" Visible="false">
                                    <fieldset class="scheduler-border fieldset_D">
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-4 pading5px">
                                                    <div class="input-group">
                                                        <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                                            NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>


                                                    </div>
                                                </div>

                                                <div class="clearfix"></div>
                                            </div>



                                        </div>

                                    </fieldset>

                                </asp:Panel>





                                <panel id="PnlDescrip" runat="server" visible="false">

                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDescrip" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea inptNoneBorder">
                                            <PagerSettings Visible="False" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo11" runat="server" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>' BorderStyle="None" BackColor="Transparent"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle BackColor="#F5F5F5" ForeColor="#000" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>

                                </panel>
                            </asp:View>

                           
                            <asp:View ID="ViewBudgetBal" runat="server">
                                <asp:Panel ID="Panel3" runat="server">

                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <div class="form-group" style="padding-left: 24px">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Material"></asp:Label>
                                                    <asp:TextBox ID="txtSrcMat" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgbtnFindMat" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindMat_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </div>

                                                <div class="col-md-3 pading5px  asitCol3">
                                                    <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="ddlPage form-control chzn-select" Width="322px">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3" style="padding-left: 80px">
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Ship Supply Date"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" CssClass=" inputtextbox"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>

                                <asp:GridView ID="gvBgdBal" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="512px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requsition No" FooterText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Req Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFareqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvareqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFprogqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Place">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdrQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Mrr. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmrrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Order Adjust">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdradjQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oadjqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFordradjqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                                <asp:Panel ID="Panelbgdbal" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lbltxtConfirmation" runat="server" CssClass="btn btn-success primaryBtn" Text="Confirmation:"
                                                Width="120px"></asp:Label>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lbltxtOpenig1" runat="server" CssClass="lblName lblTxt" Text="Budgeted Qty"></asp:Label>
                                        <asp:Label ID="lblvalBudget" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lbltxtSuppinchain" runat="server" CssClass="btn btn-success primaryBtn" Text="Supply/ In-process"
                                                Width="120px"></asp:Label>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>


                                    <div class="form-group">
                                        <asp:Label ID="lbltxtOpenig" runat="server" CssClass="lblName lblTxt" Text="Opening"></asp:Label>
                                        <asp:Label ID="lblvalOpenig" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lbltxtdqty" runat="server" CssClass="lblName lblTxt" Text="Direct"></asp:Label>
                                        <asp:Label ID="lbltxtvaldqty" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtRequisition" runat="server" CssClass="lblName lblTxt" Text="Requisition"></asp:Label>
                                        <asp:Label ID="lblvalRequisition" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lbltxttransfer" runat="server" CssClass="lblName lblTxt" Text="Transfer"></asp:Label>
                                        <asp:Label ID="lblvaltrans" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtOpenig3" runat="server" CssClass="lblName lblTxt" Text="Total Qty"></asp:Label>
                                        <asp:Label ID="lblvalTotalSupp" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtOpenig2" runat="server" CssClass="lblName lblTxt" Text="Balance"></asp:Label>
                                        <asp:Label ID="lblvalBalance" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <%-- <table style="width: 100%;">
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                               
                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtSuppinchain" runat="server" Font-Bold="True" Font-Size="14px"
                                                    ForeColor="Yellow" Style="text-align: Left; text-decoration: underline;" Text=""
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Opening" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: left" Text="Requisition" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltxtRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxttransfer" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Transfer" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvaltrans" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69"></td>
                                            <td colspan="2">
                                                <div style="width: 230px; border-bottom: 1px solid yellow;">
                                                </div>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Total Qty" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalTotalSupp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Balance" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style67" colspan="2">
                                                <div style="width: 230px; border-top: 1px solid yellow;">
                                                </div>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>--%>
                                </asp:Panel>


                            </asp:View>
                            <asp:View ID="ViewPTracking01" runat="server">

                                <asp:Panel ID="Panel5" runat="server">

                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Requisition"></asp:Label>
                                                    <asp:TextBox ID="txtSrcRequisition02" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgbtnFindReqno02" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindReqno02_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </div>

                                                <div class="col-md-3 pading5px  asitCol3">
                                                    <asp:DropDownList ID="ddlReqNo02" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>
                                        </div>
                                    </fieldset>


                                </asp:Panel>

                                <asp:GridView ID="gvPurstk" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="App. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrderRef" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Text="Total: " Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvReqQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvfappQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvforQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>


                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurstk2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        Width="1010px">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="MR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mrrno").ToString() %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="MR Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mrrref").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat"))%>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrdNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderno").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "billno").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bill Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBillRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "billref").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBillDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTotal0" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Total: " Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                                HeaderText="MRR Qty" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfMrrQty0" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="55px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle Font-Size="12px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                                HeaderText="Bill Qty" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBillQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfBillQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="55px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle Font-Size="12px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                            <asp:View ID="ViewBillTk" runat="server">

                                <asp:Panel ID="Panel6" runat="server">
                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Bill No"></asp:Label>
                                                    <asp:TextBox ID="txtBillSearch" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgbtnFindBill" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindBill_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </div>

                                                <div class="col-md-3 pading5px  asitCol3">
                                                    <asp:DropDownList ID="ddlBillno" runat="server" CssClass="chzn-select form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>
                                        </div>
                                    </fieldset>


                                </asp:Panel>
                                <div class="row  table table-responsive">
                                    <asp:GridView ID="gvPurBilltk" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                        Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGenNo0" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppDat1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrefno0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duration">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvduration" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cumulative">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvduration" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cduration")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials5" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpecification0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqty3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTotal" runat="server" Width="80px" Text="Bill Amount"></asp:Label>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppRate2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFbillamt" runat="server" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSupplier2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </FooterTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewRateCompare" runat="server">

                                <asp:Panel ID="Panel7" runat="server">
                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Materials"></asp:Label>
                                                    <asp:TextBox ID="txtMatcomSearch" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgbtnFindMatCom" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindMatCom_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </div>

                                                <div class="col-md-4 pading5px  asitCol3">
                                                    <asp:DropDownList ID="ddlMaterialscom" runat="server" CssClass="chzn-select form-control inputTxt" Width="300px">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>


                                <asp:GridView ID="gvPurMatRVar" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmaterials" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Material Name">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblheader" runat="server" Text="Material Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExelMat" runat="server" CssClass=" btn btn-warning btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgvmaterials" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="200"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvspecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="amt1">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />


                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="amt2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="amt3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt4" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt5" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt6" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt8" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt9" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt10" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt11" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="amt12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastamt12" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <%--   <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpastprice" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pastrate")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpresentprice" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prerate")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variance in %">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpricevariance" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varrate")).ToString("#,##0.00; (#,##0.000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View runat="server" ID="VBillTracking">
                                <asp:GridView ID="gvBillRegTrack" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpayid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lgvtrackDes" runat="server" Font-Size="12px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                    %>'
                                                    Width="250px"></asp:Label>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Posted Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpostdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "posteddate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusrnam" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewOrderTracking" runat="server">
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblorder" runat="server" CssClass="lblTxt lblName" Text="Order"></asp:Label>
                                                <asp:TextBox ID="txtsrchorder" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="lbtnorder" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnorder_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    ViewOrderTracking</div>

                                            </div>


                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control chzn-select inputTxt" Width="265px"></asp:DropDownList>


                                            </div>

                                            <div class="colmd-1">

                                                <asp:LinkButton ID="lbtnOkOrder" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOkOrder_Click">Ok</asp:LinkButton>

                                            </div>








                                        </div>


                                    </div>

                                </fieldset>

                                <asp:GridView ID="gvorder" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea">

                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mrf. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Chalan. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgchalanno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chalanno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mrr. ref">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgmrref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        

                                        <asp:TemplateField HeaderText="Specification ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Currency">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurrency" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpostperson" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usirdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>


                            </asp:View>








                            <%--OnRowDeleting="dgv1_RowDeleting" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating" OnRowCancelingEdit="dgv1_RowCancelingEdit"--%>

                            <asp:View ID="GenBillTrack" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvGenBillTracking" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" OnRowEditing="gvGenBillTracking_RowEditing" OnRowCancelingEdit="gvGenBillTracking_RowCancelingEdit" OnRowUpdating="gvGenBillTracking_RowUpdating"  CssClass=" table-responsive  table-hover table-bordered grvContentarea" >
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGenNo1" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="grpdesc" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdemogrpdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "demogrpdesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="slnum" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvslnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Req No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Voucher Number" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvspcfcod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Account Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Pactcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdemopactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "demopactcode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Resource Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrsircode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Rescode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdemorescode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "demorescode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppDat1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrefno1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgchlno1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                                            <asp:CommandField ShowEditButton="True" ControlStyle-Width="25px">
                                                <ControlStyle Width="35px" />
                                                <HeaderStyle Width="35px" />
                                                <ItemStyle Width="35px" />
                                            </asp:CommandField>

                                            <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprjname4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>


                                                <EditItemTemplate>
                                                    <fieldset class="scheduler-border fieldset_A">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol4">
                                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlgrdacccode" runat="server" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged" CssClass="form-control chzn-select"
                                                                            TabIndex="28" Style="width: 200px;" AutoPostBack="True">  
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </EditItemTemplate>


                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                        CssClass="btn btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="170px"></asp:Label>
                                                </ItemTemplate>


                                                <EditItemTemplate>
                                                    <fieldset class="scheduler-border fieldset_A">
                                                        <div class="form-horizontal">
                                                            <%--<div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlgrdacccode" runat="server" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged" CssClass="form-control chzn-select"
                                                                            TabIndex="28" Style="width: 130px;" AutoPostBack="True">  
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol4">
                                                                    <asp:Label ID="lblgvreshead" runat="server" CssClass="lblTxt lblName">Details Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="chzn-select"
                                                                            TabIndex="28" Style="width: 200px;">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>





                                            <%--<asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpecification1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppRate02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lgvamount2" runat="server" CssClass="form-control inputTxt"
                                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Old Amount" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoldamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oldamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSupplier02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRemark1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqnote")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusername1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                         <%--   <asp:TemplateField HeaderText="Print">
                                                <ItemTemplate>                                                    
                                                    <asp:LinkButton ID="btnPrintReqInfo1" OnClick="btnPrintReqInfo1_Click" runat="server" CssClass="btn btn-default btn-xs" Style="background-color:#C0C0C0"><span style="color:#002bff" class="fa fa-print"></span> </asp:LinkButton>

                                                </ItemTemplate>

                                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>



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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
