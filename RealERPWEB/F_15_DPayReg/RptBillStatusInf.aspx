<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBillStatusInf.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.RptBillStatusInf" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         
        });
        function pageLoaded() {

            try {

                $("input, select")
                    .bind("keydown",
                        function(event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });

                $('.chzn-select').chosen({ search_contains: true });

               
                //$('#<%=this.gvPayment.ClientID%>').tablesorter();
                

               

                
                $('#<%=this.gvPayment.ClientID%>').tblScrollable();
            }


            catch (e) {
                alert(e.message);


            }


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
                       
                        <fieldset class="scheduler-border">
                            <asp:Panel ID="PnlBill" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Receive Date"></asp:Label>
                                            <asp:TextBox ID="txtReceiveDate" OnTextChanged="txtReceiveDate_TextChanged" AutoPostBack="True" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="imgrecdate"
                                                TargetControlID="txtReceiveDate"></cc1:CalendarExtender>
<%--                                            <asp:LinkButton ID="imgrecdate" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>
                                                

                                              <asp:RadioButtonList runat="server" ID="rblpaytype" CssClass="rbtnList1 chkBoxControl" RepeatDirection="Horizontal" Visible="false">
                                                
                                                <asp:ListItem Value="Resource" Selected="True">Resource</asp:ListItem>
                                            </asp:RadioButtonList>
                                          



                                        </div>
                                        
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to" Text="Project"></asp:Label>
                                             <asp:DropDownList ID="ddlproj" runat="server" Width="280" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" Text="Resource"></asp:Label>
                                             <asp:DropDownList ID="ddlres" runat="server" Width="270" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                            
                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:LinkButton ID="btnAllBill" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="btnAllBill_Click">Show</asp:LinkButton>
                                        </div>

                                      


                                    </div>
                                    
                                </div>
                            </asp:Panel>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvPayment" runat="server"  ShowFooter="True" AutoGenerateColumns="False"
                                Style="margin-top: 0px"  CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPayment_RowDataBound">
                           
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Date<span class='glyphicon glyphicon-chevron-down'></span>" >
                                        
                                         <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn  btn-primary  btn-xs">Total</asp:LinkButton>
                                    </FooterTemplate>
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvbilldat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"  Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Bill No  <span class='glyphicon glyphicon-chevron-down'></span>">
                                         <FooterTemplate>
                                             <asp:LinkButton ID="lbtnfUpdate" runat="server" CssClass="btn btn-danger  btn-xs" OnClick="lbtnfUpdate_Click">Update</asp:LinkButton>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            
                                 

                                            <asp:HyperLink ID="hbtnvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Target="_blank"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                   

                                   <asp:TemplateField HeaderText="Head of Accounts" Visible="false">
                                        
                                       
                                        <ItemTemplate>
                                             <asp:Label ID="Labelyy2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "<span class=gvdesc>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")).Trim(): "")+ "</span>" %>'
                                            Width="180px"></asp:Label>
                                          <%--  <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="120px"></asp:Label>--%>
                                        </ItemTemplate>
                                        
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Head of Accounts  <span class='glyphicon glyphicon-chevron-down'></span>">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                 Width="230px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>




                                    <asp:TemplateField>
                                        
                                         <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Details Head "></asp:Label>
                                                    </td>
                                                    <td class="style59">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCdataExel" runat="server" CssClass=" btn btn-success btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>

                                                        <span class='glyphicon glyphicon-chevron-down'></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        
                                        
                                        
                                        
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                       <%-- <FooterTemplate>
                                            <asp:Label ID="lbltoftal" runat="server" CssClass="btn btn-danger primaryBtn"> Toatl :</asp:Label>
                                        </FooterTemplate>--%>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                   

                                 
                                    
                                    
                                     
                                    
                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="Pay Date <span class='glyphicon glyphicon-chevron-down'></span>" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvpaydat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"  
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydat")).ToString("dd-MMM-yyyy") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Revise Date <span class='glyphicon glyphicon-chevron-down'></span>" >
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvrevdat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"  
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "revdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="65px"></asp:Label>

                                                <%--  <cc1:CalendarExtender ID="txtgvrevdat_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="imgrecdate"
                                                TargetControlID="txtgvrevdat"></cc1:CalendarExtender>--%>

                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Bill Amount  <span class='glyphicon glyphicon-chevron-down'></span>">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                           <asp:Label ID="txtFTotal" runat="server" ForeColor="#000" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                         <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Cumulative Amount  <span class='glyphicon glyphicon-chevron-down'></span>">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcubillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "camt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                         <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                     
                                  
                            
                                     

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            
                           <%-- <div class="col-xs-offset-6" runat="server" Visible="False" id="total">
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltoftal" runat="server" CssClass="btn btn-danger primaryBtn"> Toatl :</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Label ID="txtFTotal" runat="server" ForeColor="#000" Font-Bold="True"></asp:Label>
                            </div>--%>
                           

                        </div>
                    </div>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

