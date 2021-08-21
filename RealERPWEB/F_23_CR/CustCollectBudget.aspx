<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustCollectBudget.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustCollectBudget" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
               <div class="container moduleItemWrpper">
                     <div class="contentPart">
                             <div class="row">  
                                     <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <asp:Panel ID="Panel1" runat="server" >
                                             <div class="form-group">
                                               <div class="col-md-6 pading5px asitCol6">
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName"  Text="Budget No.:" ></asp:Label>

                                                     <asp:Label ID="lblCurBgdNo1" runat="server" CssClass=" smLbl_to" Text="BGD00-"  ></asp:Label>

                                                    <asp:TextBox ID="txtCurBgdNo2" runat="server" CssClass=" inputtextbox">00000</asp:TextBox>

                                                   <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text=" Date:"   ></asp:Label>

                                                    <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate">  </cc1:CalendarExtender>

                                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                                </div>
                                              </div>

                                                   <div class="form-group">
                                               <div class="col-md-6 pading5px asitCol6">
                                                   <asp:LinkButton ID="lbtnPrevBudget" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnPrevBudget_Click">Prev. Budget: </span></asp:LinkButton>

                                                   <asp:DropDownList ID="ddlPrevBgdList" runat="server"  CssClass="ddlPage"  Width="267px" ></asp:DropDownList>

                                                    <asp:Label ID="lblmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>

                                                </div>
                                              </div>
                                            </asp:Panel>
                                         </div>
                                    </fieldset>
                                 </div>
                         <div class="table table-responsive">
                              <asp:GridView ID="gvCollectBud" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                            onpageindexchanging="gvCollectBud_PageIndexChanging" ShowFooter="True" 
                            Width="452px" AllowPaging="True" PageSize="15">
                         
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" ForeColor="Black" 
                                            Height="16px" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProjectName" runat="server" ForeColor="Black" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                            Width="250px" Font-Bold="True"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lFinalUpdateCost_Click"> Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdesc" runat="server" ForeColor="Black" 
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc").ToString()) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCustName" runat="server" ForeColor="Black" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                               <asp:TemplateField HeaderText="Dues Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>' 
                                           Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Target Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbgdamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
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

