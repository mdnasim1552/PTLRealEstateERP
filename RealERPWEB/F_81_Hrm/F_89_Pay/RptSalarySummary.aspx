<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalarySummary.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptSalarySummary" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    
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
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="pull-left">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-4" runat="server" id="comlist" Visible="False">
                                        <asp:label CssClass="smLbl_to" runat="server">Companies</asp:label>
                                        <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2" Width="224">
                                        </asp:DropDownList>
                                   
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="TopSheet" runat="server">
                                <asp:GridView ID="gvSalSum" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvSalSum_PageIndexChanging" ShowFooter="True"
                                    Width="500px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name" FooterText="Total:">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProName0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="#000" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cur.Emp.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpCur" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curempno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCurEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderTemplate>
                                                <asp:Label ID="lgvHTCurMEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcursal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                      <%-- <HeaderTemplate>
                                                <asp:Label ID="lgvHTCurMamt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate--%>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCurSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Bank Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurBanksal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curbankpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCurBankSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cash Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurCashsal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curcashpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCurCashSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Net </br> Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtCur" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                      <HeaderTemplate>
                                                <asp:Label ID="lgvHTCurMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCurMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre.Emp.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpPre" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preempno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTPreEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderTemplate>
                                                <asp:Label ID="lgvHTPreMEmp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPresal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prepay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <%--<HeaderTemplate>
                                                <asp:Label ID="lgvHTPreMamt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>--%>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTPreSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Bank Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpreBanksal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prebankpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTpreBankSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cash Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpreCashsal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "precashpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTpreCashSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Net </br> Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtPre" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prepay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           <HeaderTemplate>
                                                <asp:Label ID="lgvHTPreMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTPreMamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Viewtopsheet02" runat="server">
                                  <asp:GridView ID="gvgssal02" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="500px" onrowcreated="gvgssal02_RowCreated">
                                    <PagerSettings Position="Top" />
                                    <RowStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name" FooterText="Total:">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcomnamerg" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           
                                            <ItemStyle Font-Size="12px"  />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="No Of Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcempno" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curempno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lgvFcempno" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                          
                                            
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Preloan">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpreloanbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lgvFpreloanbal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                          
                                            
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Curloan">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurloanbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanbal2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lgvFcurloanbal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                          
                                            
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Gross Salary Last Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpregssalary" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pregssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpregssalary" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgspay" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgspay" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loan">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvloan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanins")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFloan" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance & Late Deduction">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvadvalateduc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tothdeduc")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFadvalateduc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF Fund">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpffund" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpffund" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payeble Before Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpaybftax" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpaybftax")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetpaybftax" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvait" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFait" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stop Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstoppayment" runat="server" Style="text-align: right" 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFstoppayment" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net Payment After Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprenetpayaftax" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, " prenetpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFprenetpayaftax" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Net Payment After Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpayaftax" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetpayaftax" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                     

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
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
