<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjTrialBalanc.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjTrialBalanc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvPrjtrbal.ClientID %>');
            var gv1 = $('#<%=this.grvTrBal2.ClientID %>');
            var gvprocost = $('#<%=this.gvprocost.ClientID %>');
            var gvprjtbal03 = $('#<%=this.gvprjtbal03.ClientID %>');

            gv.Scrollable();
            gv1.Scrollable();
            gvprocost.Scrollable();
            gvprjtbal03.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });


        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From Date:"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="  smLbl_to" Text="To:" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="1" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px  asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProjind" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass=" chzn-select form-control inputTxt" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGrp" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px">

                                         <asp:DropDownList ID="ddlProGroup" runat="server" CssClass="ddlPage"  Style="width: 460px;" TabIndex="3"></asp:DropDownList>
                                        <asp:CheckBox ID="chkdetails" runat="server" Text="Details" CssClass="btn btn-primary checkBox" Visible="false" />
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <div class=" table table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ProTrailBal" runat="server">
                                <asp:GridView ID="gvPrjtrbal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvPrjtrbal_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" HLgvDesc
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvqty" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCre" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
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
                            <asp:View ID="TrailsBal2" runat="server">
                                <asp:GridView ID="grvTrBal2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="grvTrBal2_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCre" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align: right"></asp:Label>
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
                            </asp:View>

                            <asp:View ID="viewprocost" runat="server">
                                <asp:GridView ID="gvprocost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvprocost_RowDataBound" ShowFooter="True" Width="493px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNopc" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCodepc" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDescpc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExelpc" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescpc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lvgFtotalcost" runat="server" Font-Bold="True"
                                                                Text="Total" Width="180px"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lvgFgtotalcost" runat="server" Font-Bold="True"
                                                                Text="Grand Total" Width="180px"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" HLgvDesc
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvqty" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Opening">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpeningpc" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <table>
                                                    <tr>
                                                        <td class="style58">

                                                            <asp:Label ID="lgvFOpeningpc" runat="server" Font-Size="12px" Style="text-align: right"> </asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lgvFgtocostspace" runat="server" Font-Size="12px" Text="&nbsp;"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Current">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtpc" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <table>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lgvFTDrAmtpc" runat="server" Font-Size="12px" Style="width: 100px; text-align: right"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lgvFgtocost" runat="server" Font-Size="12px" Style="width: 100px; text-align: right"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>




                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <asp:Label ID="lgvFtoAmt" runat="server" Font-Size="12px" Style="width: 100px; text-align: right"></asp:Label>

                                       




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
                            </asp:View>
                            <asp:View ID="viewProjectTriabalance03" runat="server">
                                <asp:GridView ID="gvprjtbal03" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvprjtbal03_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCodetp" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4tp" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExeltp" runat="server"
                                                                CssClass=" btn btn-danger btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesctp" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+  
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="300px">




                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="totaldb" runat="server" Font-Bold="True"
                                                                Text="Total Debit=" Width="180px"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="totalcr" runat="server" Font-Bold="True"
                                                                Text="Total Credit=" Width="180px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="netamnt" runat="server" Font-Bold="True"
                                                                Text="Net Amount=" Width="180px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" HLgvDesc
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvqty" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Opening(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnamtp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lgvFopdbamt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lgvFopCramt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <asp:Label ID="lgvFopnamtp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCreamttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing(in Tk.)">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvClsamtp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lgvFoClsdbamt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style61">
                                                            <asp:Label ID="lgvFoClsCramt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <asp:Label ID="lgvFClsamtp" runat="server" Font-Size="12px" Style="text-align: left"></asp:Label>
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
                            </asp:View>


                            <asp:View ID="viewReceiptaPayment" runat="server">
                                <asp:GridView ID="gvRecAPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvRecAPayment_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText=" Description">

                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4rec" runat="server" Font-Bold="True"
                                                                Text="Description" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnexpostexcelrp" runat="server"
                                                                CssClass=" btn  btn-success  btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblLgvDescrap" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "sgrpdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                        
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgrpdesc")).ToString()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "aaresdesc").ToString().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "sgrpdesc")).ToString().Trim().Length>0 ?   "<br>" :"") + 
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "aaresdesc")).ToString(): "")
                                                                    %>'
                                                    Width="400px">




                                                </asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Budget">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbudgetamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label ID="lgvFbgdamt" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>



                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Previous">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpream" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pream")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpream" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcuram" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoam" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTtoamp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
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
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
