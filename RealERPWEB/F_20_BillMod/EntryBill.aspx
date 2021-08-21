<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryBill.aspx.cs" Inherits="RealERPWEB.F_20_BillMod.EntryBill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
  
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                                <asp:Panel ID="PnlBill" runat="server" Width="929px">

                                  <div class="form-group">
                                    <div class="col-md-3 pading5px">

                                        <asp:Label ID="Label20" runat="server" CssClass="lblTxt lblName" Text="Receive Date"></asp:Label>
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtReceiveDate_TextChanged" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="imgrecdate" runat="server" CssClass="smLbl_to">Bill No</asp:Label>
                                        <asp:TextBox ID="txtBillno" runat="server" AutoCompleteType="Disabled" CssClass="smltxtBox"></asp:TextBox>


                                        <asp:Label ID="Label27" runat="server" CssClass="smLbl_to">Submited Amount</asp:Label>
                                        <asp:TextBox ID="txtBillAmount" AutoCompleteType="Disabled" runat="server" CssClass="inputtextbox" Style="text-align:right;"></asp:TextBox>

                                    </div>
                                 </div> 

                                  <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Project:"></asp:Label>
                                        <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt" TabIndex="2" Width="200px">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="Label30" runat="server" CssClass="lblTxt lblName" Text="Nature Of Bill"></asp:Label>
                                        <asp:LinkButton ID="ibtnnature" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnnature_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlBillNature" runat="server" CssClass=" ddlPage" Width="220px" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                  

                                </div>

                                  <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label29" runat="server" CssClass="lblTxt lblName" Text="Party Name"></asp:Label>
                                        <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindParty" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindParty_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                         </div>

                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control inputTxt"  Width="200px" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="Label31" runat="server" CssClass="lblTxt lblName" Text="Department:"></asp:Label>

                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddlPage"  TabIndex="12" > </asp:DropDownList>

                                        <asp:Label ID="Label26" runat="server" CssClass=" smLbl_to"  Text="User:"></asp:Label>

                                        <asp:TextBox ID="txtReceivedUser" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                        
                                        <asp:LinkButton ID="lbtnAddTable" runat="server"   OnClick="lbtnAddTable_Click" CssClass="btn btn-primary primaryBtn" TabIndex="13">Add Table</asp:LinkButton>
                                                                               
                                    </div>
                                    
                                </div>

                                 <div class="form-group">

                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="lblnarration" runat="server" CssClass="lblTxt lblName" Text="Naration:"></asp:Label>
                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TextMode="MultiLine" Style="width:298px;" TabIndex="4"></asp:TextBox>
                                   </div>
                                </div>

                                  <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                            <asp:LinkButton ID="lbtnRefresh" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnRefresh_Click" TabIndex="1"  Style="margin-left:40px;">Refresh</span></asp:LinkButton>

                                        <asp:Label ID="lmsg" runat="server" CssClass=" lblmsg"></asp:Label>
                                      <asp:Label ID="lblslnum" runat="server" Visible="False" CssClass="lblName"></asp:Label>
                                   </div>
                                </div>

                               </asp:Panel>

                               </div>

                        </fieldset>
                    </div>
                    <div class="table table-responsive">

                         <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                             ShowFooter="True" 
                            style="margin-top: 0px" Width="831px">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                   <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px"  
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                              

                                 <asp:TemplateField HeaderText="Bill Nature">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project Name">
                                     <FooterTemplate>
                                         <asp:LinkButton ID="lbtnTotal" runat="server"  CssClass="btn btn-primary primarygrdBtn"  onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                     </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Party Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server"  onclick="lbtnUpdate_Click" CssClass="btn btn-danger primarygrdBtn">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px"
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ref #">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Bill Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                        VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdepartment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvreceived" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "received")) %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                
                                 <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvnarration" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narration")) %>' 
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
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

                              
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

