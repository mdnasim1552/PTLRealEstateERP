<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSupListWithMat.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptSupListWithMat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%-- <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    --%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            var gv = $('#<%=this.gvSupLwMat.ClientID %>');
            gv.Scrollable();


            var gvmatwsupplier = $('#<%=this.gvmatwsupplier.ClientID %>');
            gvmatwsupplier.Scrollable();

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
                                    <div class="col-md-3 pading5px asitCol3">


                                         <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" >Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                      



                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                   
                                    

                                </div>
                                


                            </div>
                        </fieldset>
                    


                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                         
                        <asp:View ID="ViewSupplierWise" runat="server">

                            <div class="table-responsive"> 
                                <asp:GridView ID="gvSupLwMat" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" AllowPaging="True" Width="766px" PageSize="15" OnPageIndexChanging="gvSupLwMat_PageIndexChanging">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl"><ItemTemplate><asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px">

                                                                                    
                                        
                                        
                                      </asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suppliers Name"><ItemTemplate><asp:Label ID="lblgvSupName" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                Width="200px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                      <asp:TemplateField HeaderText="CONCERN PERSON"><ItemTemplate>
                                          <asp:Label ID="lblgvConcern" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                Width="200px"></asp:Label>

                                        </ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />

                                      </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Suppliers Address"><ItemTemplate><asp:Label ID="lblgvSupAddr" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addr")) %>'
                                                Width="200px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                      <asp:TemplateField HeaderText="Mobile No"><ItemTemplate><asp:Label ID="lblgvMoblile" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                Width="90"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Email"><ItemTemplate><asp:Label ID="lblgvEmail" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                Width="150px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Description"><ItemTemplate><asp:Label ID="lblgvMatDesc" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="350px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Purchase <br>Rate"><ItemTemplate><asp:Label ID="lblgvrate" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>

                            </div>
                           
                            
                            
                        </asp:View>

                           


                        <asp:View ID="ViewMatWise" runat="server">
                             <asp:GridView ID="gvmatwsupplier" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvmatwsupplier_PageIndexChanging">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl"><ItemTemplate><asp:Label ID="lblgvSlNomat" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>

                                     <asp:TemplateField HeaderText="Materials Description"><ItemTemplate><asp:Label ID="lblgvMatDescmat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                     <asp:TemplateField HeaderText="Unit"><ItemTemplate><asp:Label ID="lblgvunitmat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="50px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>


                                      <asp:TemplateField HeaderText="Specification"><ItemTemplate><asp:Label ID="lblgvspecificationmat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="120px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>


                                     <asp:TemplateField HeaderText="Rate"><ItemTemplate><asp:Label ID="lblgvratemat" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="70px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>


                                    <asp:TemplateField HeaderText="Suppliers Name"><ItemTemplate><asp:Label ID="lblgvSupNamemat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                Width="150px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Concern Person"><ItemTemplate><asp:Label ID="lblgvcpersonmat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                Width="120px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>


                                      <asp:TemplateField HeaderText="Mobile"><ItemTemplate><asp:Label ID="lblgvmobilemat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                Width="90px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                    <asp:TemplateField HeaderText="Suppliers Address"><ItemTemplate><asp:Label ID="lblgvSupAddrmat" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addr")) %>'
                                                Width="200px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>

                                    

                                   
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>




                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


