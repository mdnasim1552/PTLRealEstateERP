
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustRentMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustRentMoneyReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style51 {
            height: 16px;
        }

        .style79 {
            height: 16px;
            width: 758px;
        }

        .style80 {
            height: 16px;
            width: 70px;
        }

        .style17 {
            width: 173px;
        }

        .style18 {
            width: 560px;
        }

        .style59 {
            height: 9px;
        }

        .style60 {
            width: 97px;
            height: 9px;
        }

        .style69 {
            height: 9px;
            width: 19px;
        }

        .style61 {
            height: 9px;
            width: 121px;
        }

        .style73 {
            height: 9px;
            width: 125px;
        }

        .style62 {
            height: 28px;
        }

        .style63 {
            width: 97px;
            height: 28px;
        }

        .style70 {
            height: 28px;
            width: 19px;
        }

        .style64 {
            height: 28px;
            width: 121px;
        }

        .style74 {
            height: 28px;
            width: 125px;
        }

        .style66 {
            height: 11px;
        }

        .style76 {
            width: 121px;
        }

        .style75 {
            width: 125px;
        }

        .style82 {
            width: 97px;
        }

        .style83 {
            width: 467px;
        }

        .style84 {
            width: 302px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" CssClass="form-control inputTxt" Visible="False" runat="server"></asp:Label>

                                    </div>


                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblscustomer" CssClass="lblTxt lblName" runat="server" Text="Customer Name:"></asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCustomer_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" CssClass="form-control inputTxt" Visible="False" runat="server"></asp:Label>
                                    </div>


                                </div>
                            </div>
                        </fieldset>
                    </div>
                     <div class="row">
                        <asp:Panel ID="PnlMoneyReceipt" runat="server" Visible="False">

                            <asp:Panel ID="Panel3" runat="server" Visible="False">

                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label30" runat="server" CssClass="lblTxt lblName" Text="Previous MR:"></asp:Label>
                                            </div>

                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="form-control inputTxt" TabIndex="13">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnShowPreMrr" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowPreMrr_Click">Show</asp:LinkButton>

                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>

                                
                            </asp:Panel>
                            <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                                        

                                                        <div class="form-group">
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                 <asp:CheckBox ID="chkOrginal" runat="server" Text="Orginal " style="margin-left:20px;" Visible="False" />

                                                                <asp:CheckBox ID="chkPrevious" runat="server" AutoPostBack="True"
                                                                    CssClass=" btn chkBoxControl primaryBtn" OnCheckedChanged="chkPrevious_CheckedChanged"
                                                                    Text="Previous Mrr" />
                                                            </div>

                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Receive No"></asp:Label>
                                                                <asp:Label ID="lblReceiveNo" runat="server" CssClass="smLbl_to"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>

                                                                <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                                    AutoPostBack="True" ></asp:TextBox>

                                                                <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>


                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Replace Chq No"></asp:Label>
                                                                <asp:TextBox ID="txtRpChqNo" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-3 pading5px asitCol3">

                                                                <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName" Text="Receipt  Amount"></asp:Label>
                                                                <asp:TextBox ID="txtPaidamt" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>

                                                            </div>

                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Pay type"></asp:Label>
                                                                <asp:DropDownList ID="ddlpaytype" runat="server" Font-Bold="True" Width="95px"
                                                                    AutoPostBack="True" CssClass="ddlistPull"
                                                                    OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Text="Cheque No:"></asp:Label>

                                                                <asp:TextBox ID="txtchqno" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                                    AutoPostBack="True" ></asp:TextBox>

                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label20" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                                                <asp:TextBox ID="txtBName" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                        

                                                        <div class="form-group">
                                                            <div class="col-md-3 pading5px asitCol3">

                                                                <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Branch Name "></asp:Label>
                                                                <asp:TextBox ID="txtBranchName" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>

                                                            </div>

                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label22" runat="server" CssClass="lblTxt lblName" Text="Pay Date"></asp:Label>
                                                                  <asp:TextBox ID="txtpaydate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtpaydate_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtpaydate"></cc1:CalendarExtender>
                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label23" runat="server" CssClass="lblTxt lblName" Text="Ref. ID" ></asp:Label>

                                                                <asp:TextBox ID="txtrefid" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                                    AutoPostBack="True" ></asp:TextBox>

                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label24" runat="server" CssClass="lblTxt lblName" Text="Remarks"></asp:Label>
                                                                <asp:TextBox ID="txtremarks" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>

                                                            </div>
                                                            <div class="col-md-1 pading5px">
                                                                <asp:LinkButton ID="lblAddToTable" runat="server" OnClick="lblAddToTable_Click" CssClass="btn btn-primary primaryBtn">Add To Table</asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-3 pading5px asitCol3">

                                                                <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Collection From"></asp:Label>
                                                                <asp:TextBox ID="txtSrchCollfrm" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                                                <asp:LinkButton ID="ibtnCollfrm" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnCollfrm_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                            </div>

                                                            <div class="col-md-3 pading5px asitCol3">
                                                               <asp:DropDownList ID="ddlCollType" runat="server" CssClass="form-control inputTxt">
                                                        </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-3 pading5px asitCol3">
                                                                <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Receive Type:"></asp:Label>

                                                                <asp:DropDownList ID="ddlRecType" runat="server" CssClass="ddlPage" Width="95px" >
                                                        </asp:DropDownList>
                                                               
                                                            </div>
                                                            <div class="col-md-2">
                                                                 <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                            </div>
                                                          
                                                        </div>

                                                    </div>
                                </fieldset>


                          
                        </asp:Panel>

                    </div>

                    <div class="table table-responsive">
                        <asp:GridView ID="gvMoneyreceipt" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="gvMoneyreceipt_RowDeleting" ShowFooter="True" Width="831px"
                           >
                            <RowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Pay type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCheckno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Replace Cheque NO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrepchqno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "repchqno")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbankna" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBrance" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branchname")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay Date">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" BorderColor="White"
                                            BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbtnUpdate_Click" Style="height: 17px" TabIndex="22">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaydate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvpaydate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvpaydate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref ID">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbTotal_Click" Style="text-decoration: none;"> Total </asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrefid" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refid")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amt." Visible="True">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaidamount" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paidamount")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Received Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrectype" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "RecTyped")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </div>
            </div>




            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style51">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Style="color: #FFFFFF" Text="Project Name:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style80">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style51">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click" 
                                            TabIndex="1" />
                                    </td>
                                    <td class="style79" valign="top">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="350px" TabIndex="2" AutoPostBack="True" 
                                            onselectedindexchanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" Width="350px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" OnClick="lbtnOk_Click" 
                                            Style="color: #FFFFFF; margin-left: 0px; height: 17px;">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                    </td>
                                    <td class="style51">
                                    </td>
                                    <td class="style51">
                                    </td>
                                    <td class="style51">
                                    </td>
                                    <td class="style51">
                                    </td>
                                    <td class="style51">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style51">
                                        <asp:Label ID="lblscustomer" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Style="color: #FFFFFF" Text="Customer Name:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style80">
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="txtboxformat" 
                                            Width="80px" TabIndex="3"></asp:TextBox>
                                    </td>
                                    <td class="style51">
                                        <asp:ImageButton ID="ibtnFindCustomer" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindCustomer_Click" 
                                            TabIndex="4" />
                                    </td>
                                    <td class="style79" valign="top">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="350px" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" runat="server" BackColor="White" Font-Size="12px" 
                                            ForeColor="Blue" Height="16px" Visible="False" Width="350px"></asp:Label>
                                    </td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <%--<asp:Panel ID="PnlMoneyReceipt" runat="server" Visible="False" Width="909px">
                            <table style="width: 100%; background: #cccc99">
                                <tr>
                                    <td class="style33" colspan="10">
                                        <asp:Panel ID="Panel3" runat="server" BorderColor="#660033" BorderStyle="Solid"
                                            BorderWidth="1px" Visible="False">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td align="right" class="style17">
                                                        <asp:Label ID="Label15" runat="server" CssClass="lbltextColor"
                                                            Text="Previous MR:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style18">
                                                        <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="ddl" Font-Bold="True"
                                                            Font-Size="12px" Width="200px" TabIndex="6">
                                                        </asp:DropDownList>
                                                        <asp:LinkButton ID="lbtnShowPreMrr" runat="server" BackColor="#003366"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnShowPreMrr_Click"
                                                            Style="width: 33px" TabIndex="7">Show</asp:LinkButton>
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
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkOrginal" runat="server" BackColor="Maroon"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Text="Orginal " Visible="False"
                                            Width="105px" />
                                    </td>
                                    <td class="style82" colspan="2">
                                        <asp:CheckBox ID="chkPrevious" runat="server" AutoPostBack="True"
                                            CssClass="lbltextColor" OnCheckedChanged="chkPrevious_CheckedChanged"
                                            Text="Previous Mrr" TabIndex="9" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" CssClass="lbltextColor"
                                            Text="Receive No:" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReceiveNo" runat="server" BackColor="White"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="Label6" runat="server" CssClass="lbltextColor"
                                            Text="Receive Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label17" runat="server" CssClass="lbltextColor"
                                            Text="Replace No:" Width="115px"></asp:Label>
                                    </td>
                                    <td class="style76">

                                        <asp:TextBox ID="txtRpChqNo" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" TabIndex="11"
                                            Width="100px" CssClass="ddl"></asp:TextBox>
                                    </td>
                                    <td class="style75"></td>
                                </tr>
                                <tr>
                                    <td class="style59">
                                        <asp:Label ID="Label7" runat="server" CssClass="lbltextColor"
                                            Text="Paid Amount:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style60" colspan="2">
                                        <asp:TextBox ID="txtPaidamt" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="12"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label8" runat="server" CssClass="lbltextColor" Text="Pay type:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:DropDownList ID="ddlpaytype" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" Width="100px" TabIndex="13"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style69">
                                        <asp:Label ID="Label9" runat="server" CssClass="lbltextColor" Text="Cheque No:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:TextBox ID="txtchqno" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="14"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label10" runat="server" CssClass="lbltextColor"
                                            Text="Bank Name:" Width="115px"></asp:Label>
                                    </td>
                                    <td class="style61">
                                        <asp:TextBox ID="txtBName" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="15"></asp:TextBox>
                                    </td>
                                    <td class="style73">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style62">
                                        <asp:Label ID="Label11" runat="server" CssClass="lbltextColor"
                                            Text="Branch Name :" Width="85px"></asp:Label>
                                    </td>
                                    <td class="style63" colspan="2">
                                        <asp:TextBox ID="txtBranchName" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="16"></asp:TextBox>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="Label12" runat="server" CssClass="lbltextColor" Text="Pay Date:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtpaydate" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="17"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtpaydate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtpaydate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style70">
                                        <asp:Label ID="Label13" runat="server" CssClass="lbltextColor" Text="Ref. ID:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtrefid" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="18"></asp:TextBox>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="Label14" runat="server" CssClass="lbltextColor" Text="Remarks:"
                                            Width="115px"></asp:Label>
                                    </td>
                                    <td class="style64">
                                        <asp:TextBox ID="txtremarks" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            Width="100px" TabIndex="19"></asp:TextBox>
                                    </td>
                                    <td class="style74">
                                        <asp:LinkButton ID="lblAddToTable" runat="server" BackColor="#003366" Width="60px"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" ForeColor="#000"
                                            OnClick="lblAddToTable_Click" Style="font-size: small; height: 17px;"
                                            TabIndex="21">Add Table</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" CssClass="lbltextColor"
                                            Text="Collection From:" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSrchCollfrm" runat="server" AutoCompleteType="Disabled" CssClass="ddl"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnCollfrm" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnCollfrm_Click" />
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlCollType" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" TabIndex="13" Width="195px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="Label20" runat="server" CssClass="lbltextColor"
                                            Text="Receive Type:" Width="85px"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:DropDownList ID="ddlRecType" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" TabIndex="13" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style43">
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td class="style76">&nbsp;</td>
                                    <td class="style75"></td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td style="margin-left: 40px" colspan="12">
                        
                    </td>
                </tr>

                <tr>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td class="style83" style="margin-left: 40px"></td>
                    <td style="margin-left: 40px"></td>
                    <td style="margin-left: 40px">&nbsp;</td>
                    <td style="margin-left: 40px"></td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

