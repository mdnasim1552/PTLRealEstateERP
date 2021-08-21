<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPettyCashApp.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPettyCashApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
          $('.chzn-select').chosen({ search_contains: true });
       };

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                  <div class="col-md-1 ">

                                     <asp:Label ID="lblcurVounum" runat="server" CssClass="smLbl_to"> Employe</asp:Label>
                                      </div>
                                    <div class="col-md-3 pading5px">                                       
                                        <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control inputTxt"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        
                                        <div class="colMdbtn pading5px ">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                        </div>
                                          <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">PCBL NO.</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="smltxtBox" Text="PCB00-" ReadOnly="True" TabIndex="2"></asp:Label>
                                        <asp:TextBox ID="txtCurNo2" runat="server" CssClass="smltxtBox60px" ReadOnly="True" TabIndex="3">00000</asp:TextBox>

                                    </div>
                                 
                                     <div class="col-md-2 pading5px asitCol2 pull-right">
                                        <asp:LinkButton ID="imgPreVious" runat="server" CssClass="lblTxt lblName" OnClick="imgPreVious_Click"
                                            TabIndex="3">Previous BILL</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevList" runat="server" CssClass=" ddlPage  inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                  
                                </div>
                                <asp:Panel ID="AccPanel" runat="server" Visible="false">

                              
                                  <div class="form-group">
                                                <div class="col-md-1">
                                                      <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="lblTxt" OnClick="lnkAcccode_Click" TabIndex="12"> Head A/C</asp:LinkButton>
                                                                                                      
                                                </div>
                                                <div class="col-md-3 pading5px">
                                                    <asp:DropDownList ID="ddlacccode" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlacccode_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-md-1 pading5px">
                                                    <asp:LinkButton ID="LbtnAdd" runat="server" CssClass="btn btn-primary  okBtn" OnClick="LbtnAdd_Click" TabIndex="21">Add</asp:LinkButton>



                                                </div>
                                            



                                            </div>

                                  <div class="form-group">
                                                <div class="col-md-1">
                                                     <asp:LinkButton ID="lnkRescode" runat="server" CssClass="lblTxt " OnClick="lnkRescode_Click" TabIndex="15">Sub of A/C</asp:LinkButton>
         
                                                </div>

                                                <div class="col-md-3 pading5px ">
                                                    <asp:DropDownList ID="ddlresuorcecode" runat="server" TabIndex="16"  CssClass=" form-control inputTxt chzn-select">
                                                    </asp:DropDownList>

                                                </div>
                                      <div class="col-md-5">
                                             <asp:Label ID="Label2" runat="server" CssClass="smLbl" Text="Bill No"></asp:Label>
                                           <asp:TextBox ID="txtBillno" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    
                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl" Text="Bill date"></asp:Label>
                                        <asp:TextBox ID="TxtBillDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtBillDate">
                                        </cc1:CalendarExtender>

                                           <asp:Label ID="Label3" runat="server" CssClass="smLbl" Text="Amount"></asp:Label>
                                           <asp:TextBox ID="txtamount" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>
                                      

                                            </div>
                               </asp:Panel>
                            </div>
                        </fieldset>

                    
                         <asp:GridView ID="gvpetty" runat="server"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                               
                                OnRowDeleting="gvpetty_RowDeleting">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />
                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Head">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnCalculate" OnClick="lbtnCalculate_Click" runat="server" CssClass="btn btn-xs btn-default">Calculate</asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkbtnRecalculate" runat="server" OnClick="lnkbtnRecalculate_Click" CssClass="btn btn-xs btn-danger">Calculate</asp:LinkButton>--%>
                                        </FooterTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSirdesc" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-xs btn-success">Update</asp:LinkButton>
                                          </FooterTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bill Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbilldate" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:TextBox>
                                               <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbilldate">
                                        </cc1:CalendarExtender>
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtBillno" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtParticulars" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partculrs")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>

                                                              

                                
                                    <asp:TemplateField HeaderText="Amount" >
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtamount" runat="server"
                                                Style="font-size: 11px; text-align: right;" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Style="text-align: left" BackColor="Transparent" BorderColor="#4286f4" BorderWidth="0" BorderStyle="Solid"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
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


