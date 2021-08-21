<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkEmpMonthWiseEvaDet.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.LinkEmpMonthWiseEvaDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/jquery-ui.css" rel="stylesheet" />

    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {

            $('#<%=this.txtSrchSalesTeam.ClientID%>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded();



        });

      
    </script>


 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                    </div>


                                    <div class="col-md-2 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>



                                </div>

                                <div class="form-group">

                                    <div class="col-md-2 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                        </div>
                                    </div>



                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlEmpid" runat="server" CssClass=" form-control inputTxt"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-1 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" TabIndex="4" CssClass="btn btn-primary  primaryBtn" Text="Ok"></asp:LinkButton>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>


                                    </div>


                                </div>

                            </div>
                        </fieldset>


                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewSales" runat="server">

                            <div class=" col-md-12 pading5px">
                                <asp:Label ID="lblHeaderSales" runat="server" CssClass="smLbl_to pading5px" Text="01. SALES" Visible="false"></asp:Label>

                            </div>
                            <div class="clearfix"></div>
                            <asp:GridView ID="gvSales" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlcall" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="28" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>







                                    <asp:TemplateField HeaderText="Sales Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprojectsales" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"pactdesc")) %>'
                                                Width="150px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCuName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalesdatesales" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"tdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFSaleamt" runat="server" Width="80px"></asp:Label></FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalesamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt")).ToString("#,##0;(#,##0);") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>



                            <div class="col-md-12 pading5px">
                                <asp:Label ID="lblHeaderColl" runat="server" CssClass=" smLbl_to pading5px" Text="02. COLLECTION" Visible="false"></asp:Label>

                            </div>
                            <div class="clearfix"></div>
                            <asp:GridView ID="gvCollection" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlcall" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="28" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MR No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMRNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MR Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrdatecoll" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"tdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprojectcoll" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"pactdesc")) %>'
                                                Width="150px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Name ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCuName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnDescoll" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvChNo" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBaNo" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bank Brabch">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbbranch" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvChDat" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chqdate")).ToString("dd-MMM-yyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="MR Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFMramtcoll" runat="server" Width="80px"></asp:Label></FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmramtcoll" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt")).ToString("#,##0;(#,##0);") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <div class="col-md-12 pading5px">
                                <asp:Label ID="lblHeaderOffer" runat="server" CssClass=" smLbl_to pading5px" Text="03. Followup" Visible="false"></asp:Label>

                            </div>
                            <div class="clearfix"></div>
                            <asp:GridView ID="gvOffer" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSloff" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="28" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Followup" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClientoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"grpdesc")) %>'
                                                Width="120px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Client Name" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClientoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosdesc")) %>'
                                                Width="120px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblphoneoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"phone")) %>'
                                                Width="100px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meeting Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmdateoff" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"meetdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Center"   Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprojectoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"pactdesc")) %>'
                                                Width="100px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit & Size"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunitoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"uausize")) %>'
                                                Width="100px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Asking Price"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaskpriceoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"askprice")) %>'
                                                Width="70px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Offer Price" HeaderStyle-Width="60px"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloffpriceoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"offprice")) %>'
                                                Width="70px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Amount"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbookamtoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"bookam")) %>'
                                                Width="70px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GAP %"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdfifinpercntoff" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"gap")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discussion">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldiscussionoff" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"discuss")) %>'
                                                Width="200px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appoinment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblappoinmentcall" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"nappdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="60px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
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

                        <asp:View ID="ViewCollection" runat="server">
                        </asp:View>
                        <asp:View ID="ViewOthers" runat="server">
                            <asp:GridView ID="gvdetailsg" runat="server"
                                AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlgen" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="28" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdategen" runat="server"
                                                Text='<%# (DataBinder.Eval(Container.DataItem,"exdate1")) %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Taget <br/>Setup">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsetgen" runat="server"
                                                Text='<%# (DataBinder.Eval(Container.DataItem,"tarmon")) %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Activities">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactivitiesgen" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"actdesc")) %>'
                                                Width="300px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Quantity">
                                   
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"exqty")).ToString("#,##0;(#,##0);") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Note">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvnotegen" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"note")) %>'
                                                Width="250px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvremarks" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"remarks")) %>'
                                                Width="150px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
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


                        <asp:View ID="viewLegal" runat="server">
                            <asp:GridView ID="gvdetailslg" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px" >
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Case Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcasename" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Activities">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactivities" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wsirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Court Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcourtname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                       <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                           
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussionpdisc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdatpdis" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
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


