<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPaymetDueAllPrj.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPaymetDueAllPrj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style type="text/css">
.switch {
  position: relative;
  display: inline-block;
  width: 40px;
  height: 20px;
}

.switch input { 
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 18px;
  width: 18px;
  left: 1px;
  bottom: 1px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(18px);
  -ms-transform: translateX(18px);
  transform: translateX(18px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 20px;
}

.slider.round:before {
  border-radius: 50%;
}
        </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:calendarextender id="txtDatefrom_CalendarExtender" runat="server" format="dd-MMM-yyyy"
                                                              targetcontrolid="txtDatefrom" enabled="true"></cc1:calendarextender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:calendarextender id="txtDateto_CalendarExtender" runat="server" format="dd-MMM-yyyy"
                                                              targetcontrolid="txtDateto" enabled="true"></cc1:calendarextender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-7  pading5px">

                                                
                                             <label id="chkbod"  runat="server"
                                                  class="switch">
                                            <asp:CheckBox ID="ChboxDetails" runat="server" Checked="true"/>

                                                 <span class="btn btn-xs slider round"></span>
                                                 </label>
                                            <asp:Label runat="server" Text="Details"></asp:Label>
                                           
                                             
                                         


                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        
                        <asp:GridView ID="gvAcc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" PageSize="15" ShowFooter="True" Width="841px" OnRowDataBound="gvAcc_OnRowDataBound" OnRowCreated="gvAcc_RowCreated">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblAccDesc" runat="server" CssClass="GridLebel" 
                                                   
                                                    
                                                     Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) +                                                                 
                                                    Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    
                                                     Width="250" Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LnkfTotal" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000">Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfqty" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdqty" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdrat" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkftmat" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdam" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField  HeaderText="Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfactqty" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblpurqty" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqty")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                            
                                        
                                        <asp:TemplateField  HeaderText="Rate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblpurrat" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purrat")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfacttamt" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpuramt" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puramt")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Paid" >
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpayp" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpayamt" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Pay Due">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpaydue" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaydue" runat="server" BackColor="Green" ForeColor="White"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paydue")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Total Due">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpaytmt" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpayduet" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paytdue")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
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

