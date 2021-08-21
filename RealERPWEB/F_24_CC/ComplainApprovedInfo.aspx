<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ComplainApprovedInfo.aspx.cs" Inherits="RealERPWEB.F_24_CC.ComplainApprovedInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            <%--var gvSpledger = $('#<%=this.gvSpledger.ClientID %>');
            var gvSPayment = $('#<%=this.gvSPayment.ClientID %>');
            var gvAllSupPay = $('#<%=this.gvAllSupPay.ClientID %>');
            var gvSPayment02 = $('#<%=this.gvSPayment02.ClientID %>');
            gvSpledger.Scrollable();
            gvSPayment.Scrollable();
            gvAllSupPay.Scrollable();
            gvSPayment02.Scrollable();--%>


          <%--  var gv = $('#<%=this.grvMWiseSupBill.ClientID %>');
            gv.Scrollable();--%>
        }

     </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Complain No</asp:Label>
                                        <asp:TextBox ID="txtSrcComp" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindComplain" runat="server" CssClass="btn btn-primary srearchBtn" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlComplain" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" onclick="lnkbtnOk_Click">ok</asp:LinkButton>

                                    </div>

                                <div class="col-md-3 pull-right">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                            </div>


                                </div>
                            
                        </fieldset>
                    </div>
                           <div class="table-responsive table">
                                <asp:GridView ID="grvcomplain" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"  AllowPaging="true"
                                    ShowFooter="True" Width="832px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Complain Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladvamt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                                                           
                                  <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkvmrno" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbok" runat="server" Width="30px" CommandArgument="lbok" OnClick="lbok_Click" >OK</asp:LinkButton>
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


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


