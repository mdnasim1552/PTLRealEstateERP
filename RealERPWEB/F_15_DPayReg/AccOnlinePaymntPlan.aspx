<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymntPlan.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.AccOnlinePaymntPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>

     <style>

          .grvHeader th {
            text-align:center;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border">
                            <asp:Panel ID="PnlBill" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Receive Date"></asp:Label>

                                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="inputTxt inputName" Width="70px" AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>

                                        </div>

                                        <div class="col-md-10">
                                            <asp:LinkButton ID="Okbtn" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="Okbtn_Click">Show Data</asp:LinkButton>

                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                        </div>

                                    </div>

                                </div>
                            </asp:Panel>
                        </fieldset>

                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Style="margin-top: 0px" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPayment_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Payment Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                
                                 

                                  <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Bill Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvmslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                               



                                <asp:TemplateField HeaderText="Value Date">
                                    <ItemTemplate>

                                          <asp:Label ID="lnkgvvaldate" runat="server"
                                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "valdate")).ToString("dd-MMM-yyy") %>'
                                                                            Width="70px"></asp:Label>

                                  

                                    </ItemTemplate>
                                  
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Received Date">
                                    <FooterTemplate>
                                        <%-- <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>--%>
                                    </FooterTemplate>
                                    <ItemTemplate>

                                          <asp:Label ID="txtgvrcvdate" runat="server"
                                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyy") %>'
                                                                            Width="60px"></asp:Label>

                                      
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>



                                
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details Head">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtopayamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpayamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                          
                                <asp:TemplateField HeaderText="Payment Date">
                                    <ItemTemplate>
                                    


                                          <asp:Label ID="txtgvpaymentdate" runat="server"
                                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyy") %>'
                                                                            Width="60px"></asp:Label>

                                    </ItemTemplate>
                                  
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                              
                                


                                <asp:TemplateField HeaderText="Bill Nature" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnaturedsc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                



                                <asp:TemplateField HeaderText="Active">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallView_CheckedChanged" />
                                    </HeaderTemplate>
                                     
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpayplan"))=="True" %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

