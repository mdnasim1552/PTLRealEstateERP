<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBillTrackingSearch.aspx.cs" Inherits="RealERPWEB.F_20_BillMod.RptBillTrackingSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

           <%-- $('#<%=this.txtrcvDate.ClientID %>').focus();--%>
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtPenBillStatus(event);



            });


            var gv1 = $('#<%=this.gvPayment.ClientID %>');
            gv1.Scrollable();




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
                                        <div class="col-md-12 ">

                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>
                                            <asp:TextBox ID="txtfrmdate" runat="server" AutoPostBack="True"
                                                CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>


                                            <asp:Label ID="lblfrmdate0" runat="server" CssClass="lblTxt lblName" Text="To" Width="130"></asp:Label>
                                            <asp:TextBox ID="txttoDate" runat="server" AutoPostBack="True"
                                                CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12 ">
                                            <%--<asp:Label ID="lblRcvDate" runat="server" CssClass="lblTxt lblName">Received Date</asp:Label>
                                            <asp--%><%--:TextBox ID="txtrcvDate" runat="server" OnTextChanged="txtrcvDate_TextChanged" CssClass="inputtextbox"></asp:TextBox> --%>
                                             <asp:Label ID="lblIssueNo" runat="server" CssClass="lblTxt lblName" Text="Issue No:"></asp:Label>
                                            <asp:TextBox ID="txtissueno" runat="server" TabIndex="12" CssClass="inputtextbox"></asp:TextBox>
                                            <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName" Text="Department:" Width="130"></asp:Label>
                                            <asp:TextBox ID="TxtDept" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                                    
                                         
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRefnum" runat="server" CssClass="lblTxt lblName" Text="Ref Number:"></asp:Label>
                                            <asp:TextBox ID="txtRefnum" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblBillNature" runat="server" CssClass="lblTxt lblName" Text="Nature Of Bill:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtnofbill" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>

                                
                                    <div class="form-group">

                                  <div class="col-md-12">
                                      <%-- <asp:Label ID="lblTranDate" runat="server" CssClass="lblTxt lblName">Transeer Date:</asp:Label>
                                            <asp:TextBox ID="txtTranDate" runat="server" OnTextChanged="txtTranDate_TextChanged" CssClass="inputtextbox"></asp:TextBox>--%>
                                      
                                     <asp:Label ID="lblTranDate" runat="server" CssClass="lblTxt lblName" Text="Transfer Date:"></asp:Label>
                                            <asp:TextBox ID="txtTranDate" runat="server" AutoPostBack="True"
                                                CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTranDate"></cc1:CalendarExtender>

                                <asp:Label ID="lblTranDept" runat="server" CssClass="lblTxt lblName" Text="Transfer Dept:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtTranDept" runat="server" TabIndex="12" CssClass="inputtextbox"></asp:TextBox>

                                           <%-- <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkRefresh_Click" TabIndex="50" Text="Refresh" CssClass="btn btn-primary okBtn" Width="80"></asp:LinkButton>--%>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblpartyName" runat="server" CssClass="lblTxt lblName" Text="Party Name:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtpartyName" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkOk_Click" TabIndex="20" Text="Ok" CssClass="btn btn-primary okBtn"></asp:LinkButton>

                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>
          
                              <%--            <div class="form-group">
                                        <div class="col-md-12">
      
                                            <%--<asp:TextBox ID="txtcptpdate" runat="server" AutoPostBack="True" OnTextChanged="txtcptpdate_TextChanged" TabIndex="10" CssClass="inputtextbox"></asp:TextBox>

                                            <%--<asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" TabIndex="20" Text="Ok" CssClass="btn btn-primary okBtn"></asp:LinkButton>--%>
                                           <%-- <div class=" clearfix"></div>
                                        </div>
                                    </div>--%>


                                        <div class="col-md-12">
                                            <asp:Label ID="lblBillAmount" runat="server" CssClass="lblTxt lblName" Text="Bill Amount:"></asp:Label>
                                            <asp:TextBox ID="txtBillamount" runat="server" TabIndex="9" CssClass="inputtextbox"></asp:TextBox>



                                            <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click" TabIndex="50"  Text="Refresh" CssClass="btn btn-primary okBtn" Width="80"></asp:LinkButton>

                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            
                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>


                              

                            </div>
                        </fieldset>
                        </div>
                         <div class="table table-responsive">

                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="margin-top: 0px" Width="831px">
                                   
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
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy")%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvdeptname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc"))%>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Nature">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvref" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submitted Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvsubamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Approved  Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFappamt" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvappamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreceived" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "received")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvtrndate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trndate"))%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Transfer Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvdtndeptname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdeptdesc"))%>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvnarraion" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narration"))%>' 
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
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
                     
                     
               
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->  

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


