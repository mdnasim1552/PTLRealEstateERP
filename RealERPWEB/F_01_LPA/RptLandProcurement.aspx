<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLandProcurement.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptLandProcurement" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>

                                        <asp:TextBox ID="txtAccHead" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="imgBtnAccHead" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgBtnAccHead_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlAcHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-5  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date"></asp:Label>

                                        <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click" Text="Show"></asp:LinkButton>
                                    </div>


                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class=" form-group">
                                <asp:Label ID="lblLandProcurement" runat="server" Text="Land Procurement:" CssClass="btn btn-success primaryBtn" Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>


                            <div class="table table-responsive">
                                <asp:GridView ID="gvlandSt" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvlandSt_RowDataBound"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="text-align: left" Width="600px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" 
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-Font-Size="12px" HeaderText="Description of Account">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescryptionbe" runat="server" Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="230px"></asp:Label>
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
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftbgdqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bgd. </Br>Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget </Br>Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfbgdam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbgdam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual </Br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftToQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual </Br> Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual </Br> Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblftoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance </Br> Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvavqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftavqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance </Br> Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvavrat" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance </Br> Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftAvAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
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
                            <hr class="hrline" />

                            <div class="form-group">
                                <asp:Label ID="lblPaymentStatus" runat="server" Text="Payment Status:" Visible="False" CssClass="btn btn-success primaryBtn"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvpayment" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
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
                                            <asp:Label ID="lgvFbillamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
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
                                            <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
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
                                            <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
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

                            <hr class="hrline" />
                            <div class="form-group">
                                <asp:Label ID="lblNotes" runat="server" Text="Notes:" CssClass="btn btn-success primaryBtn" Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvNote" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
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


                            <hr class="hrline" />



                        </asp:View>
                        <asp:View ID="DetailsBgd" runat="server">
                            <asp:GridView ID="gvlandStsum" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvlandStsum_RowDataBound"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Style="text-align: left" Width="654px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Description of Account">
                                        <ItemTemplate>



                                            <asp:HyperLink ID="lkgvgvprocess" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim(): "") 
                                            
                                            
                                             %>'
                                                Width="280px">
                                            </asp:HyperLink>



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
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavqtysum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle Font-Size="12px" HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Rate" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavratsum" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Balance Amt">
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
