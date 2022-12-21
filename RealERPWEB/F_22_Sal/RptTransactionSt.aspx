
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ASITMaster.Master"   CodeBehind="RptTransactionSt.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptTransactionSt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
      <script type="text/javascript" language="javascript">
          $(document).ready(function () {
              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          });
          function pageLoaded() {


              var WorkOrdHisSup = $('#<%=this.gvSercharge.ClientID%>');
              WorkOrdHisSup.gridviewScroll({
                  width: 1160,
                  height: 420,
                  arrowsize: 30,
                  railsize: 16,
                  barsize: 8,
                  varrowtopimg: "../Image/arrowvt.png",
                  varrowbottomimg: "../Image/arrowvb.png",
                  harrowleftimg: "../Image/arrowhl.png",
                  harrowrightimg: "../Image/arrowhr.png",
                  freezesize: 11
              });



       
              $('#<%=this.grvTrnDatWise.ClientID%>').gridviewScroll({
                  width: 1160,
                  height: 420,
                  arrowsize: 30,
                  railsize: 16,
                  barsize: 8,
                  varrowtopimg: "../Image/arrowvt.png",
                  varrowbottomimg: "../Image/arrowvb.png",
                  harrowleftimg: "../Image/arrowhl.png",
                  harrowrightimg: "../Image/arrowhr.png",
                  freezesize: 11


              });



              $('#<%=this.gvTransSum.ClientID%>').gridviewScroll({
                  width: 1160,
                  height: 420,
                  arrowsize: 30,
                  railsize: 16,
                  barsize: 8,
                  varrowtopimg: "../Image/arrowvt.png",
                  varrowbottomimg: "../Image/arrowvb.png",
                  harrowleftimg: "../Image/arrowhl.png",
                  harrowrightimg: "../Image/arrowhr.png",
                  freezesize: 9


              });

              
      


              $('.chzn-select').chosen({ search_contains: true });

              //$('#rpRTypeTransTb').Scrollable({

              //});


        }
      </script>


    <style>
        .table td, .table th {
            padding: 0rem;
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
                                    <div class="col-md-3 asitCol3 pading5px">                                   
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" CssClass="rbtnList1"
                                            RepeatColumns="7"
                                            RepeatDirection="Horizontal" Style="text-align: left" Width="800px"
                                            Visible="False">
                                            <asp:ListItem>With Post Dated</asp:ListItem>
                                            <asp:ListItem>Current Dated</asp:ListItem>
                                            <asp:ListItem>Actual Dated</asp:ListItem>
                                            <asp:ListItem>Collection Statement</asp:ListItem>
                                            <asp:ListItem>Entry Date </asp:ListItem>
                                            <asp:ListItem>Cheque In Hand </asp:ListItem>
                                            <asp:ListItem>Cash In Hand </asp:ListItem>



                                        </asp:RadioButtonList>

                                    </div>

                                </div>
                                    
                                <div class="form-group">

                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="lblProName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" BorderStyle="None"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click"
                                            TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 asitCol5 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="ddlPage chzn-select" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName"
                                            Text="From :"></asp:Label>

                                        <asp:TextBox ID="txtfromdate" runat="server"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy "
                                            TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-5 asitCol5 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txttodate"></cc1:CalendarExtender>
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                            Text="Page Size:"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            CssClass="ddlPage" Width="95px"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn primaryBtn btn-primary"
                                            TabIndex="3"
                                            Font-Underline="False">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="TransPrjWise" runat="server">
                                <div class=" table table-responsive">
                                    <asp:GridView ID="gvDaTrns" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        OnPageIndexChanging="gvDaTrns_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcProDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Text="Total: " Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
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

                                            <asp:TemplateField HeaderText="MR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcMRNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcMRDat" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
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
                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvChDat" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chqdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCaAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvChAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvCheAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reconciliation Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRecDat" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntrydate" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUserName" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
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
                            </asp:View>


                            <asp:View ID="TransDateWise" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="grvTrnDatWise" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        OnPageIndexChanging="grvTrnDatWise_PageIndexChanging"
                                        OnRowDataBound="grvTrnDatWise_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Group Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcGrpt" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcMRNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="MR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcMRDat" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            
                                    <asp:TemplateField HeaderText="Project Description">

                                             <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Project Description" Width="180px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgcProDesc" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"  Text="Total"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Project Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcProDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collection From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCollFrm" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name ">
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCuName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmobileNo" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobileno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
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
                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvChDat" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCaAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvChAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>


                                                <FooterTemplate>

                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>


                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reconciliation Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRecDat1" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntrydate" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUserName" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
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
                            </asp:View>


                            <asp:View ID="ClientStatus" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="grvClientStat" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        OnPageIndexChanging="grvClientStat_PageIndexChanging" Width="906px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcProDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCuName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Text="Total: " Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvQty" runat="server" Style="text-align: Right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sold Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSoldVal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSoldVal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRecAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Receivable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReceivable" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFReceivable" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bank Clearance Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReconAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Not Clearance Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvNReconAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duereconamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFNReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
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
                            </asp:View>


                            <asp:View ID="RepChqNo" runat="server">


                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                    Text="Mrr No :"></asp:Label>

                                <asp:TextBox ID="txtSrcMrrNo" runat="server" BorderStyle="None"
                                    CssClass="inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindRepNo" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindRepNo_Click"
                                    TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                <div class="table table-responsive">
                                    <asp:GridView ID="grvRepChq" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        OnPageIndexChanging="grvRepChq_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcProDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Text="Total: " Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
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

                                            <asp:TemplateField HeaderText="Org MR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcMRNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgmrno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Org Chq Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcOrgChqDat" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orgdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Org Cheque No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvChNo" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgchqno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Org Cheque Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrgChAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orgamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOrgCheAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rep Mrno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRepChqNo" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rep. Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRepChDat" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chqdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Replace Chq No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUserName" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Replace Ch Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRChqAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFRepCheAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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



                            <asp:View ID="ProSummary" runat="server">
                                <div class="table table-responsive ">
                                  <asp:GridView ID="gvTransSum" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvTransSum_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNopsum" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Project Name">

                                             <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Project Name" Width="150px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExcel2" runat="server"
                                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgcProDesc" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"  Text="Total"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <%--  <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvActDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                        Width="150px" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="amt1">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt1" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt1" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt2" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt2" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt3" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt3" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt4" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt4" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt5" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt5" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt6" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt6" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt7" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt7" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt8">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt8" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt8" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt9">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt9" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt9" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt10" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt10" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt11">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt11" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt11" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt12" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt12" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="amt13">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt13" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt13")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt13" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt14">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt14" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt14")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt14" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="amt15">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt15" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt15")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt15" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="amt16">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt15" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt16")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt16" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt17">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt17" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt17")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt17" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt18">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt18" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt18")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt18" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt19">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt19" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt19")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt19" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt20" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt20")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt20" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt21">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt21" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt21")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt21" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt22">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt22" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt22")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt22" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="amt23">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt23" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt23")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt23" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="amt24">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt24" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt24")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt24" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="amt25">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt25" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt25")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt25" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="amt26">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt26" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt26")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt26" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="amt27">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt27" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt27")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt27" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="amt28">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt28" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt28")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt28" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="amt29">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt29" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt29")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt29" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="amt30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt30" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt30")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt30" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="amt31">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt31" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt31")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt31" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                           <asp:TemplateField HeaderText="amt32">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt32" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt32")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt32" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            
                                            
                                           
                                           

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnetamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnetamt" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="amt33">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt33" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt33")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt33" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>




<%--                                            <asp:TemplateField HeaderText="amt34">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamt34" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt23")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt34" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>






                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    </div>
                                 
                            </asp:View>


                            <asp:View ID="ViewRtypeWiseTran" runat="server">





                                <asp:Repeater ID="rpRTypeTrans" runat="server" OnItemDataBound="rpRTypeTrans_ItemDataBound">

                                    <HeaderTemplate>
                                        <table id="rpRTypeTransTb" class=" table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                <th>Project Description</th>
                                                <th>Unit Description</th>
                                                <th>Sales</th>
                                                <th>Reg & Vat</th>

      
                                              <%--  <th >Utility Charge</th>--%>
                                                <th>
                                                <asp:Label ID="lblrputility" runat="server" Text="Utility Charge"></asp:Label>
                                                   </th>
                                                <th>Additional Work</th>
                                            <%--    <th>Association Fee</th>--%>
                                                <th>
                                                <asp:Label ID="lblassocialtion" runat="server" Text="Association Fee"></asp:Label>
                                                    </hr>
                                                <th>Society Fee</th>
                                                <th>Service Charge</th>
                                                <th>Delay Charge</th>
                                                <th>Others</th>
                                                <th>Total</th>

                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lgcProDescrtype" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="180px">
                                                     
                                                     
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lgvUnDestype" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lgvsalesamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sales")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lgvregavat" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "regavat")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lgvsolar" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spanel")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>


                                            <td>
                                                <asp:Label ID="lgvaddwork" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adwork")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>


                                            <td>
                                                
                                                <asp:Label ID="lblrpassociation" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "associafee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>

                                             
                                            <td>
                                                <asp:Label ID="lgvsocietyfee" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "societyfee")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>


                                            <td>
                                                <asp:Label ID="lblrpsercharge" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sercharge")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lgvdelaycharge" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delcharge")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lgvothers" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lgvtotalrtype" runat="server" Font-Bold="true" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></td>

                                        </tr>

                                    </ItemTemplate>

                                    <FooterTemplate>

                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                          
                                        </tr>


                                        </table>
                                    </FooterTemplate>





                                </asp:Repeater>



                            </asp:View>



                            <asp:View ID="Associaltion" runat="server">
                                <div class="table table-responsive ">
                                    <asp:GridView ID="gvAssociation" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvAssociation_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNopsum" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <ItemTemplate>
                                          
                                                <asp:Label ID="lgcProDescrtype" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="180px">
                                                     
                                                     
                                                </asp:Label>
                                                </ItemTemplate>
                                            

                                               <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReceipt" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                        Width="140px" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Association</br> Fee">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFassofee" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAssofee" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assotarget")).ToString("#,##0;(#,##0); ")%>'
                                                        Width="70px" styte="text-align:right" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>    


                                            <asp:TemplateField HeaderText="Received">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFReceipt" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReceipt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asoreceipt")).ToString("#,##0;(#,##0); ")%>'
                                                        Width="70px" styte="text-align:right" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Receivable">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFReceivable" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReceivable" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ")%>'
                                                        Width="70px" styte="text-align:right" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpayment" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPayment" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asopayment")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBalance" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbalance" runat="server" Font-Size="11px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            
                                           


                                            
                                            
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <div class="table table-responsive ">
                            </asp:View>
                             <asp:View ID="ViewSerCharge" runat="server">
                                  <asp:GridView ID="gvSercharge" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px" OnRowDataBound="gvSercharge_RowDataBound" OnPageIndexChanging="gvSercharge_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Project">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdescmpay" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "")  %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Unit">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))
                                                                                                       %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>



                                   



                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopening" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFopening" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcollamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcollamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Payment Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoamtmpay" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topayamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoamtmpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                         
                                <asp:View ID="ViewServicePayment" runat="server">
                                <div class="table-responsive">
                                <asp:GridView ID="gvServicePayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px" OnPageIndexChanging="gvServicePayment_PageIndexChanging" >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Project">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdescmpay" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "")  %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Unit">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))
                                                                                                       %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>



                                   



<%--                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopening" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFopening" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


<%--                                     <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcollamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcollamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Total Payment Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoamtmpay" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topayamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoamtmpay01" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                            <%--         <asp:TemplateField HeaderText="Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>





                                    <asp:TemplateField HeaderText="amt1">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay1" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay1" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay2" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay2" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay3" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay3" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay4" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay4" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay5" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay5" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay6" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay6" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay7" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay7" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay8" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay8" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay9" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay9" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay10" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay10" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay11" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay11" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay12" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay12" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                                  
                            </asp:View>

                            <asp:View ID="ViewServiceCollection" runat="server">
                                <div class="table-responsive">
                                <asp:GridView ID="gvServiceCollection" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px" OnPageIndexChanging="gvServiceCollection_PageIndexChanging" >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Project">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdescmpay" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "")  %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Unit">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))
                                                                                                       %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>



                                   



                                  <%--  <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopening" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFopening" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                                     <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcollamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcollamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                  <%--  <asp:TemplateField HeaderText="Total Payment Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoamtmpay" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topayamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoamtmpay01" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                            <%--         <asp:TemplateField HeaderText="Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>





                                    <asp:TemplateField HeaderText="amt1">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay1" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay1" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay2" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay2" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay3" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay3" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay4" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay4" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay5" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay5" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay6" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay6" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay7" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay7" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay8" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay8" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay9" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay9" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay10" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay10" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay11" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay11" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtmpay12" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtmpay12" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                                  
                            </asp:View>
                            
                            <asp:View ID="ViewModificationCharge" runat="server">
                                  <asp:GridView ID="gvModCharge" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px" OnPageIndexChanging="gvModCharge_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Project">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdescmpay" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "custname").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")).Trim(): "")  %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Unit">


                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))
                                                                                                       %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>



                                   



                                    <asp:TemplateField HeaderText="Modification</br> Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvModcharge" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modcharge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFModCharge" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRecieved" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modreceipt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFReceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalance" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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

                            <asp:View ID="RectypeWise02" runat="server">
                                  <asp:GridView ID="gvRectypeWise02" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px" OnPageIndexChanging="gvRectypeWise02_PageIndexChanging" OnRowDataBound="gvRectypeWise02_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSl56" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Project Name">

                                             <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Project Name" Width="150px"></asp:Label>


                                                        <asp:HyperLink ID="hlbotherCdataExcel" runat="server"
                                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetailspactdesc" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"  Text="Total"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                               <%--     <asp:TemplateField HeaderText=" Project">

                                        <ItemTemplate>
                                             <asp:Label ID="lblgvdetailspactdesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))
                                                                                                       %>'
                                                Width="200px"></asp:Label>

                                        </ItemTemplate>
                                        


                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                    </asp:TemplateField>--%>

                                    
                                     <asp:TemplateField HeaderText="Pactcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvRectypactcode" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))
                                                                                                       %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Usircode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="gvRectusircode" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode"))
                                                                                                       %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText=" Customer Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="gvRectypeWise02Custname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                                                       %>'
                                                Width="120px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Unit Description">
                                        <ItemTemplate>
                                            <asp:Label ID="gvRectypeWise02unitname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))
                                                                                                       %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgvRectypeWisesales" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sales")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWisesales" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Reg & Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWiseregvat" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "regavat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWiseregvat" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Solar Panel">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWisesolarpanel" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spanel")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWisesolarpanel" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Additional Work ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWiseadwrk" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adwork")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWiseadwrk" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Society Fee ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWisesociteyfee" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "societyfee")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWisesociteyfe" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Delay Charge ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWisedelaycharge" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delcharge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWisedelaycharge" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer Fee">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWiseTransfer" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "transfee")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWiseTransfer" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWiseOther" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWiseOther" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Commercial Receive">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWiseCommer" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comrecv")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWiseCommer" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRectypeWisetotal" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRectypeWisetotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                  ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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




