<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurInterComMatTransfer.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurInterComMatTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
   
</script>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
      

        });
        function pageLoaded() {

          


    

            $('.chzn-select').chosen({ search_contains: true });
         



        };
    </script> 

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Transfer From"></asp:Label>
                                        <asp:Label ID="lblFromCmpName" runat="server" CssClass="form-control inputTxt" Width="218px"></asp:Label>


                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:RadioButtonList ID="rbtnList1" BorderColor="BlueViolet" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl form-control" RepeatColumns="5">
                                            <asp:ListItem>Transfer From</asp:ListItem>
                                            <asp:ListItem>Transfer To</asp:ListItem>
                                        </asp:RadioButtonList>

                                         <%--<div class="col-md-2 pull-right">--%>
                                       
                                            <%--<asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" OnClick="lnkNextbtn_Click"><span class="flaticon-add118"></span> Next</asp:LinkButton>--%>

                             <%--       </div>--%>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                        
                                         <asp:Label ID="lblfVoucherNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                                        <asp:TextBox ID="txtfdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfdate_CalendarExtender" runat="server"  
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                               <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">Project </asp:Label>
                                    <asp:TextBox ID="txtSrcfrmproject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindfrmproject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindfrmproject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control dataLblview chzn-select" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                </div>


                            </div>
                                
                                <div class="form-group">
                                         <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Resource List:</asp:Label>
                                            <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindRes_Click" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslist" runat="server" CssClass="form-control inputTxt chzn-select"  AutoPostBack="true" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>

                                     <div class="col-md-5 pading5px ">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="ddlPage62 " style="width:150px;" >
                                            </asp:DropDownList>
                                            <asp:Label ID="lblfQty" runat="server" CssClass=" smLbl_to">Qty</asp:Label>
                                         <asp:TextBox ID="txtfqty" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                             <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselect_Click" >Select</asp:LinkButton>
                                              <div class="col-md-2 pull-right">
                                             <%--<a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>--%>
                                        
                                            <asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" OnClick="lnkNextbtn_Click"><span class="flaticon-add118"></span> Reload</asp:LinkButton>

                                    </div>
                                        
                                        </div>
                                      
                                 
                                    </div>


                                <div class="form-group">

                                       <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblAccountHead" runat="server" CssClass="lblTxt lblName"> Inter Company </asp:Label>
                                        <asp:TextBox ID="txtserheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindAccount_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                       
                                      

                                    </div>

                           

                                   





                                   
                                </div>

                        </fieldset>


                             <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                               

                                    <asp:TemplateField HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                                CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Transfer Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           

                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt" runat="server"
                                                Style="font-size: 11px; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblRefNum0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnRefresh_Click">Refresh</asp:LinkButton>

                                    </div>




                                </div>
                            </div>

                        </fieldset>

                    </div>




                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Transfer To"></asp:Label>
                                        <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" Width="218px">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                        <asp:Label ID="lbltVoucherNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:Label>

                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                                        <asp:TextBox ID="txttdate" runat="server" CssClass="inputTxt inputName inPixedWidth120"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttdate_CalendarExtender3" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>
                                    <div class="col-md-4 pading5px">

                                    </div>
                                </div>


                                 <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lbltoProject" runat="server" CssClass="lblTxt lblName">Project </asp:Label>
                                    <asp:TextBox ID="txtscrhtoProject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindtoproject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindtoproject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control  inputTxt"  >
                                    </asp:DropDownList>
                                  
                                </div>
                                     </div>

                                     <div class="form-group">
                                         <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblresource" runat="server" CssClass="lblTxt lblName">Resource List:</asp:Label>
                                            <asp:TextBox ID="txtResource" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="btnsource" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="btnsource_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control inputTxt chzn-select"  AutoPostBack="true" OnSelectedIndexChanged="ddlResource_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>

                                     <div class="col-md-5 pading5px ">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlspecfi" runat="server" CssClass="ddlPage62" style="width:150px;" >
                                            </asp:DropDownList>
                                            <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to">Qty</asp:Label>
                                         <asp:TextBox ID="txtqtyto" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                             <asp:LinkButton ID="btnselect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="btnselect_Click" >Select</asp:LinkButton>
                                        </div>
                                      
                                 
                                    </div>
                                    
                                  
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblAccountHead0" runat="server" CssClass="lblTxt lblName">Inter Company</asp:Label>
                                        <asp:TextBox ID="txtsertoheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindtoAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindtoAccount_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlAcctoHead" runat="server" CssClass="form-control inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                  

                                </div>
                            </div>

                        </fieldset>

                        <asp:GridView ID="gvaccto" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidto" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="false" />
                                    <asp:TemplateField HeaderText=" resourcecodeto" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCodeto" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                               

                                    <asp:TemplateField HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcodto" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspecificationto" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1311" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnktotalto" runat="server" Font-Bold="True"
                                                CssClass="btn btn-primary primaryBtn" OnClick="lnktotalto_Click">Total</asp:LinkButton>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Transfer Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqtyto" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           

                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrateto" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmountto" runat="server" Style="text-align: right"
                                                Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblamtto" runat="server"
                                                Style="font-size: 11px; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum1" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        <asp:Label ID="lbltRefNum" runat="server" CssClass="inputtextbox"></asp:Label>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblSrInfo0" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txttSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                    </div>
                                    <div>
                                        <asp:Label ID="lblComAdd" runat="server" Visible="False"></asp:Label>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblRefNum2" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txttNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>

                                   
                                          <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                         </div>




                                </div>
                            </div>

                        </fieldset>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>



<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>




