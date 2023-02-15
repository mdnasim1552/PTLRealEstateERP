<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProjectCollBrkDown.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptProjectCollBrkDown" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvIndPrjDet.ClientID %>');
            gv.Scrollable();



        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">



                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewColPrjWise" runat="server">
                            <div class="row">



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server">Project Name</asp:Label>
                                        <asp:Label ID="lblActDesc" runat="server" CssClass="btn btn-sm btn-primary primaryBtn"></asp:Label>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server">Date</asp:Label>
                                        <asp:Label ID="lblDate" runat="server" CssClass="btn btn-sm btn-primary primaryBtn"></asp:Label>
                                    </div>

                                </div>


                            </div>
                            <asp:GridView ID="gvPrjWiseCollBrkDown" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="785px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvPrjWiseCollBrkDown_RowDataBound">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActcode" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))  %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Usircode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUsircod" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode"))  %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Description" FooterText="Total:">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc" runat="server" AutoCompleteType="Disabled" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))  %>'
                                                Width="120px"></asp:HyperLink>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="Black" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc1" runat="server" AutoCompleteType="Disabled" Target="_blank" Font-Underline="false" ForeColor="blue"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname"))  %>'
                                                Width="120px"></asp:HyperLink>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Value">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSaVal" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSaVal" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Encash">
                                        <FooterTemplate>
                                            <asp:Label ID="lgFClrAmt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgClrAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tclramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Returned Cheque">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Today's">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Post Dated Cheque">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>








                                    <asp:TemplateField HeaderText="Total Received Value">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmtrep" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecev")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Non-Operating Income">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFNOI" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvNOI" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STD Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFStdAmt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvStdAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancel Unit Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcuamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcuamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Received">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                    </asp:TemplateField>

                                </Columns>

                               <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <RowStyle CssClass="grvRowsNew" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>






                        </asp:View>
                        <asp:View ID="ViewClientLedger" runat="server">


                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                                <asp:Label ID="LblPrjDesc" runat="server" CssClass=" smLbl_to"></asp:Label>
                                                <asp:Label ID="Label12" runat="server" CssClass="smLbl_to">Customer Name</asp:Label>
                                                <asp:Label ID="lblCustName" runat="server" CssClass="smLbl_to"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" CssClass="smLbl_to">Date</asp:Label>
                                                <asp:Label ID="lblDate1" runat="server" CssClass="smLbl_to"></asp:Label>

                                            </div>

                                        </div>


                                    </div>

                                </fieldset>
                            </div>


                        </asp:View>
                        <asp:View ID="ViewIndPrjDetails" runat="server">


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server">Project Name</asp:Label>
                                        <asp:Label ID="lblIndPrjDesc" runat="server" CssClass="btn btn-sm btn-primary primaryBtn"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server">Date</asp:Label>
                                        <asp:Label ID="lblIndDate" runat="server" CssClass="btn btn-sm btn-primary primaryBtn"></asp:Label>
                                    </div>

                                </div>




                            </div>

                            <asp:GridView ID="gvIndPrjDet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                OnRowDataBound="gvIndPrjDet_RowDataBound" ShowFooter="True">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<span style=color:blue><B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B></span>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                </Columns>
                               <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <RowStyle CssClass="grvRowsNew" />
                                <HeaderStyle CssClass="grvHeaderNew" />

                            </asp:GridView>





                        </asp:View>
                        <asp:View ID="ViewSubLedger" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                                <asp:Label ID="lblLGPrjDesc" runat="server" CssClass=" smLbl_to  btn btn-primary primaryBtn"></asp:Label>
                                                <asp:Label ID="lblLGResDesc" runat="server" CssClass="smLbl_to ">Resource Code</asp:Label>
                                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to  btn btn-primary primaryBtn"></asp:Label>

                                                <asp:Label ID="lblDate12" runat="server" CssClass="smLbl_to">Date</asp:Label>
                                                <asp:Label ID="lblLGDate" runat="server" CssClass="smLbl_to btn btn-primary primaryBtn"></asp:Label>

                                            </div>

                                        </div>


                                    </div>

                                </fieldset>
                            </div>

                            <asp:GridView ID="gvSPLedger" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" OnRowDataBound="gvSPLedger_RowDataBound">

                                <Columns>
                                    <asp:TemplateField HeaderText="Group Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvGrpDesc" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vou.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvoudate" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" Font-Bold="true" ForeColor="Black" Font-Size="12px" Style="text-align: left;"
                                                Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="85px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldescription0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                                Width="250px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmount0" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmount0" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalamt" runat="server" Width="80px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="HLgvRemarks" runat="server" Width="80px" ForeColor="Black" Font-Size="11px" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>




                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                </Columns>
                               <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <RowStyle CssClass="grvRowsNew" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>




                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

