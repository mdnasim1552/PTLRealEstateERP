<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjTrialBalanc.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjTrialBalanc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

            var gv = $('#<%=this.gvPrjtrbal.ClientID %>');
            var gv1 = $('#<%=this.grvTrBal2.ClientID %>');
            var gvprocost = $('#<%=this.gvprocost.ClientID %>');
            var gvprjtbal03 = $('#<%=this.gvprjtbal03.ClientID %>');

            //gv.Scrollable();
            //gv1.Scrollable();
            //gvprocost.Scrollable();
            //gvprjtbal03.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });


        }

    </script>
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 30px !important;
        }
       
        .mt20 {
            margin-top: 20px;
        }
        .mt22 {
            margin-top: 21px;
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

            <div class="card card-fluid mb-1">
                <div class="card-body mb-0">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass=" form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                             </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divtodate" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName" Text="To" Visible="false"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                             </div>
                        </div>                        
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divprjname" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass=" form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindProjind" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2">&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass=" chzn-select form-control form-control-sm" TabIndex="3"></asp:DropDownList>
                              </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divdetais" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblGrp" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 mt20" id="divprjgroup" runat="server">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlProGroup" runat="server" CssClass="chzn-select form-control form-control-sm "  TabIndex="3"></asp:DropDownList>                                
                              </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 mt20" id="divchkdetails" runat="server" Visible="false">
                            <div class="form-group">
                                 <asp:CheckBox ID="chkdetails" runat="server" Text="Details" CssClass="btn btn-primary btn-sm checkBox" Visible="false" />
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                              </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body mb-4" style="min-height:520px">                  
                    <div class="table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ProTrailBal" runat="server">
                                <asp:GridView ID="gvPrjtrbal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    OnRowDataBound="gvPrjtrbal_RowDataBound" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts"></asp:Label>&nbsp;
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass=" btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>'></asp:HyperLink>
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
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="TrailsBal2" runat="server">
                                <asp:GridView ID="grvTrBal2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    OnRowDataBound="grvTrBal2_RowDataBound" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="10px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>'></asp:Label>
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

                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="viewprocost" runat="server">
                                <asp:GridView ID="gvprocost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    OnRowDataBound="gvprocost_RowDataBound" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNopc" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCodepc" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
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
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description Of Accounts"></asp:Label></td>
                                                &nbsp;
                                                <asp:HyperLink ID="hlbtntbCdataExelpc" runat="server" CssClass=" btn btn-success btn-xs" ToolTip="Export to Excel" Style="text-align: center" ><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescpc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) %>'></asp:HyperLink>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="lvgFtotalcost" runat="server" Font-Bold="True"
                                                                Text="Total" ></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lvgFgtotalcost" runat="server" Font-Bold="True"
                                                                Text="Grand Total"></asp:Label>
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
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="viewProjectTriabalance03" runat="server">
                                <asp:GridView ID="gvprjtbal03" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    OnRowDataBound="gvprjtbal03_RowDataBound" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCodetp" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                            <asp:Label ID="Label4tp" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts"></asp:Label>&nbsp;
                                                            <asp:HyperLink ID="hlbtntbCdataExeltp" runat="server"
                                                                CssClass=" btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesctp" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+  
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="totaldb" runat="server" Font-Bold="True"
                                                                Text="Total Debit="></asp:Label>

                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="totalcr" runat="server" Font-Bold="True"
                                                                Text="Total Credit="></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="netamnt" runat="server" Font-Bold="True"
                                                                Text="Net Amount="></asp:Label>
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
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
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
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="100px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCreamttp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCrAmttp" runat="server" Font-Size="12px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="100px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing(in Tk.)">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvClsamtp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
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
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="100px"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="viewReceiptaPayment" runat="server">
                                <asp:GridView ID="gvRecAPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    OnRowDataBound="gvRecAPayment_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4rec" runat="server" Font-Bold="True"
                                                    Text="Description"></asp:Label>&nbsp;
                                                <asp:HyperLink ID="hlbtnexpostexcelrp" runat="server"
                                                    CssClass=" btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
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
                                                                    %>'>
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
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
