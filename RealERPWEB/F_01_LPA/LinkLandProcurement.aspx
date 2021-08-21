<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkLandProcurement.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LinkLandProcurement" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">

                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:Label ID="lblProjectName" runat="server" CssClass=" smLbl_to" ></asp:Label>

                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-12">

                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:Label ID="lblDuration" runat="server" CssClass="smLbl_to"></asp:Label>

                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">

                            <div class="form-group">

                                <asp:Label ID="lblLandProcurement" runat="server" Text="Land Procurement:" CssClass="btn btn-success primaryBtn" Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="table table-responsive">
                            <asp:GridView ID="gvlandSt" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvlandSt_RowDataBound"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="654px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-Font-Size="12px" HeaderText="Description of Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescryptionbe" runat="server" Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                Width="280px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnitbe" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Broker">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBroker" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "brodesc").ToString() %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgdqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftbgdqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bgd. Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfbgdam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvToqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftToQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblftoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftavqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Rate" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavrat" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftAvAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>

                            </div>
                            <div class="form-group">

                                <asp:Label ID="lblPaymentStatus" runat="server" Text="Payment Status" CssClass="btn btn-success primaryBtn" Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="table table-responsive">
                            <asp:GridView ID="gvpayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNcf0" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsupdesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc"))
                                                                        
                                                                         
                                                                    %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbillamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbillamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                                </div>
                            <div class="form-group">

                                <asp:Label ID="lblNotes" runat="server" Text="Notes" CssClass="btn btn-success primaryBtn" Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvNote" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNcf" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "ndesc"))
                                                                        
                                                                         
                                                                    %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted">
                                        <ItemTemplate>
                                            <asp:Label ID="lgbalambb" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                        <asp:View ID="DetailsBgd" runat="server">
                            <div class="table table-responsive">
                            <asp:GridView ID="gvlandStsum" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvlandStsum_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="654px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Description of Account">
                                        <ItemTemplate>



                                            <asp:Label ID="lkgvgvprocess" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim(): "") 
                                            
                                            
                                             %>'
                                                Width="280px">
                                            </asp:Label>



                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgdqtysum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bgd. Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgRatesum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Amt" ItemStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdamsum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvToqtysum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRatesum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Amt" ItemStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClamsum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail. Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavqtysum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail.Rate" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavratsum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Available.Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAamtsum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPersum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>
                                </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
