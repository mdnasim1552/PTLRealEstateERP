<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjPcash.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptPrjPcash" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



            var gv = $('#<%=this.gvPtcash.ClientID %>');
            gv.Scrollable();
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-8 pading5px asitCol8">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true">
                                        </cc1:CalendarExtender>


                                         <div class="col-md-1">
                                            <asp:LinkButton ID="lnkdetail" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkdetail_Click" >Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" > Bank List</asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px  asitCol5">
                                        <asp:DropDownList ID="ddlReslist" runat="server" Width="300px" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        </div>

                            

                                    <%--<div class="col-md-1">--%>

                                 <%--       <div class="col-md-1">
                                            <asp:LinkButton ID="lnkdetail" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkdetail_Click">Ok</asp:LinkButton>

                                        </div>--%>



                             <%--       </div>--%>

                                </div>

                                </div>
                            <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblprj" runat="server" CssClass="lblTxt lblName">Project List</asp:Label>
                                        <asp:TextBox ID="txtshrprj" runat="server" CssClass=" inputtextbox" ></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindPrj" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrj_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlPrjlist" runat="server" CssClass="form-control inputTxt"  Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>
                        
                    <div class="clearfix"></div>
              

             </div>

                    </fieldset>
                    
                     
     <asp:GridView ID="gvPtcash" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="431px" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                              

                                 <asp:TemplateField HeaderText="Description">
                                      
                                   
                                    <ItemTemplate>
                                        <asp:textBox ID="txtgvclientc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="310px" ></asp:textBox>
                                    </ItemTemplate>
                                     
                                      
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Unit">
                                    
                                    
                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblFTotal" runat="server" Font-Bold="True"
                                            Font-Size="12px" CssClass="btn btn-primary primaryBtn"> Total </asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Amount">
                                      
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;#,##0.00; ") %>'
                                            Width="90px" ></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                                <asp:Label ID="lblFamount" runat="server" Font-Bold="True" Font-Size="12px" Width="90px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                   
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
    
           </div>
               
        
        </ContentTemplate>
    </asp:UpdatePanel>

        

</asp:Content>

