<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccTopPage.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccTopPage" %>

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
                                 
                                    <div class="col-md-3 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>
                                  

                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        
                                        <div class="colMdbtn pading5px ">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                        </div>
                                        <div class="pull-right">
                                    <asp:HyperLink ID="HpblnkNew" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccPettyCashApp.aspx?Type=Entry&genno=" CssClass="btn btn-xs btn-success"> NEW BILL</asp:HyperLink>
                                            </div>
                                    </div>
                                 
                                   
                                  
                                </div>
                               
                            </div>
                        </fieldset>

                    
                         <asp:GridView ID="gvpetty" runat="server"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px" OnRowDataBound="gvpetty_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                            <asp:Label ID="lblpcbl" runat="server"
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcblno1")) %>'></asp:Label>
                                            <asp:Label ID="lblpcbl1" runat="server"
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcblno")) %>'></asp:Label>
                                            <asp:Label ID="lblvounum" runat="server"
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="PC BILL NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpcbl" runat="server" Width="90px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcblno1")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvEmp" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                      
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                  
                                     <asp:TemplateField HeaderText="Bill Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbilldate" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                            
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Item ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbliTem" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                      <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                                                                            

                                
                                    <asp:TemplateField HeaderText="Amount" >
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lblamt" runat="server"
                                                Style="font-size: 11px; text-align: right;" BackColor="Transparent" BorderWidth="0"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                            VerticalAlign="Middle" Font-Size="12px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypApprv" runat="server" Target="_blank"  CssClass="btn btn-xs"><span class="glyphicon glyphicon-print"></span>
                                                </asp:HyperLink>
                                              
                                           </ItemTemplate>  
                                           <ItemStyle Width="80px" />
                                           <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top"/>
                                        </asp:TemplateField>


                                      <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="hlnkdlt" OnClick="hlnkdlt_OnClick" Visible="False" runat="server" CssClass="btn btn-xs"><span class="glyphicon glyphicon-trash"></span>
                                                </asp:LinkButton>
                                           </ItemTemplate>  
                                           <ItemStyle Width="80px" />
                                           <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top"/>
                                        </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                               
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

