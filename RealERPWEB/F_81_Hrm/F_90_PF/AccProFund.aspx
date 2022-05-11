 <%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccProFund.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccProFund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="JS/FormValidation.js"></script>
    <%--    <link href="../../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
        .table td, .table th {
    padding: 0.2rem!important;
  
}
    </style>



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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

            <div class="card mt-5">
                <div class="card-header">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row">

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblPFMonth" runat="server" Text="Month:"></asp:Label>

                                    <asp:DropDownList ID="ddlyearmon" CssClass="form-control form-control-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblDate" runat="server" Text="Voucher Date:"></asp:Label>

                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm">00000</asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server">Company</asp:Label>


                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="lblcurVounum" runat="server" Text="CV No:"></asp:Label>
                                <div class="form-group  form-inline">


                                    <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="form-control  form-control-sm" Width="80"></asp:TextBox>

                                    <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="form-control  form-control-sm" Width="80" ToolTip="You Can Change Voucher Number."></asp:TextBox>
     

                                </div>
                            </div>

        
                            <div class="col-lg-1">
                                <asp:LinkButton ID="lbtnoK" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk">Ok</asp:LinkButton>

                            </div>





                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                
                              
                                           <asp:CheckBox ID="chkcomshare" runat="server" AutoPostBack="True" OnCheckedChanged="chkcomshare_CheckedChanged" Text="Company Contribution"
                                        Visible="False" />
                            </div>
                                    <div class="col-lg-3">
                                <asp:Panel ID="pnlgen" runat="server" Visible="false">
              

                                    <div class="input-group ">
                                        <asp:TextBox ID="txtCompShare" runat="server" CssClass=" form-control form-control-sm" MaxLength="3"></asp:TextBox>

                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary  btn-sm" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
                <div class="card-body">
                    <div class="table table-responsive">
                        <asp:GridView ID="gvPfAcc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" 
                            OnRowDeleting="gvPfAcc_RowDeleting">
                            <PagerSettings Position="Top" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"  Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />


                                <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Description " Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>


                                        <asp:Label ID="lgvDesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "section").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim(): "")
                                                                    %>'
                                            Width="300px" Style="font-size: 12px; color: Black; text-decoration: none;">    </asp:Label>



                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Card">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server" CssClass="btn btn-success
                                            btn-sm" OnClick="lbtnFinalUpdate_Click"
                                            Style="text-align: center">Update</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lgvCard" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbtnTotal_Click" Style="text-align: center">Total:</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvname" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dr")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" Font-Size="11px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDr" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cr")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" Font-Size="11px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCr" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrmarks" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rmarks")) %>'
                                            Width="150px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
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

                    </div>
                         <asp:Panel ID="pnlNaration" runat="server" Visible="false">
                <div class="row mt-2">
                    <div class="col-lg-6 mr-auto">
                        <div class="form-group row">

                        <asp:Label ID="lblRefNum" runat="server" CssClass="col-4" Text="Ref./Cheq No/Slip No."></asp:Label>

                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control col-8" AutoCompleteType="Disabled"></asp:TextBox>


              
                </div>
                <div class="form-group row">
                    
                        <asp:Label ID="lblSrInfo" runat="server" CssClass=" col-4" Text="Other ref.(if any)"></asp:Label>

                        <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" CssClass="form-control col-8" ></asp:TextBox>
                </div>

                <div class="form-group row">
   
                        <asp:Label ID="lblNaration" runat="server" CssClass=" col-4" Text="Narration:"></asp:Label>

                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" CssClass="form-control col-8"
                            TextMode="MultiLine"></asp:TextBox>



    
                </div>
                    </div>
                </div>
                
            </asp:Panel>
                </div>
            </div>


            <%--       <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-11 pading5px asitCol11">


                                            <asp:Label ID="lblPFMonth" runat="server" CssClass="lblTxt lblName" Text="Month:"></asp:Label>

                                            <asp:DropDownList ID="ddlyearmon" CssClass="ddlPage" runat="server">
                                               
                                            </asp:DropDownList>



                                            <asp:Label ID="lblDate" runat="server" CssClass=" smLbl_to" Text="Voucher Date:"></asp:Label>

                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox">00000</asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>



                                            <asp:LinkButton ID="lbtnoK" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName" Text="CV No:"></asp:Label>

                                            <asp:TextBox ID="txtcurrentvou" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="inputtextbox" ToolTip="You Can Change Voucher Number."></asp:TextBox>
                                            <asp:CheckBox ID="chkcomshare" runat="server" AutoPostBack="True" OnCheckedChanged="chkcomshare_CheckedChanged" Text="Company Contribution"
                                                Visible="False" />
                                            <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass=" btn btn-danger  primaryBtn pull-right"></asp:Label>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlgen" runat="server" Visible="false">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lbllstVouno0" runat="server" CssClass="lblTxt lblName" Width="120px" Style="font-size: 10px; margin-left: -10px" Text="Company Share In %"></asp:Label>
                                            <asp:TextBox ID="txtCompShare" runat="server" CssClass=" inputtextbox" MaxLength="3"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>

                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </asp:Panel>

                                <div class="form-group">
                                    <div class="col-md-12">



                                        <div class="clearfix"></div>
                                    </div>
                                </div>

                            </div>
                        </fieldset>--%>



       


            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
