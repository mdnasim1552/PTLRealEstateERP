<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptConstruProgressSum.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptConstruProgressSum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


         
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                            
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" TabIndex="3" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"  TabIndex="9"></asp:LinkButton>

                                    </div>
                                     

                                     <div class="col-md-3  pull-right">
                                     <asp:UpdateProgress ID="UpdateProgress1"  runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="lblpln" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>

                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                              </div>
                                   


                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvConPro" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="378px" PageSize="20"
                            OnPageIndexChanging="gvConPro_PageIndexChanging"
                            OnRowDataBound="gvConPro_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                           
                                <asp:TemplateField HeaderText="Project">
                                    <FooterTemplate>

                                        <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px" Text="Total"></asp:Label>



                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="false" Target="_blank" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="220px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Budgeted Amount">
                                    <ItemTemplate>
                                        
                                      
                                        
                                        <asp:HyperLink ID="hlnkgvBudgetAmt" runat="server" Style="text-align: right"  Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plan Completed">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMasPlan" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFMasPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target Work As Of Today">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMPlanastoday" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFMPlanastoday" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Execution Amt.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnklgvExAmt" runat="server" Style="text-align: right"  Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eamt")).ToString("#,##0;(#,##0); ")%>'
                                            Width="90px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Less Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlessExAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:HyperLink ID="hlnkgvFlessexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="Blue" Target="_blank" Style="text-align: right" Width="120px"></asp:HyperLink>
                                    </FooterTemplate>


                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Target Work in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvprcentontw" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontw")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual Work in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvprcentonaw" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronac")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Less Progress in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvprcentonlp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronlp")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

