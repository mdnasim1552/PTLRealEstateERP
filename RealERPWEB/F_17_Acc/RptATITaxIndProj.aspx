<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptATITaxIndProj.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptATITaxIndProj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>


            var gvaitvsd = $('#<%=this.gvaitvsd.ClientID %>');

            gvaitvsd.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 3
            });

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
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                    </div>
                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Resource"></asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">

                                        <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>



                                    </div>


                                </div>

                                

                                


                            </div>
                        </fieldset>                        
                    </div>

                    <div class="table-responsive">

                         <asp:GridView ID="gvaitvsd" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="518px">
                            
                            <RowStyle />

                            <Columns>

                                <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px">Total :</asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Project-1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj1" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFprj1" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj2" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFprj2" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj3" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj3" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-4">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj4" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj4" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-5">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj5" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj5" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj6" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##000;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj6" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-7">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprj7" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFprj7" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-8">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj8" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj8" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-9">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj9" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj9" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project-10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj10" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj10" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj11" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj11" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Project-12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj12" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj12" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj13" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj13" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj14" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj14" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                
                                 <asp:TemplateField HeaderText="Project-15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj15" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj15" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj16" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj16" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj17" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj17" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj18" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj18" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj19" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj19" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj20" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj20" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj21" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj21" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj22" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj22" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>




                                       <asp:TemplateField HeaderText="Project-23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj23" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj23" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj24" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj24" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj25" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj25" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj26" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("#,##000;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj26" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprj27" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFprj27" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj28" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj28" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj29" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj29" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project-30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj30" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj30" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj31" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj31" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Project-32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj32" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj32" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-33">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj33" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj33" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-34">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj34" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj34" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                
                                 <asp:TemplateField HeaderText="Project-35">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj35" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj35" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-36">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj36" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj36" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-37">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj37" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj37" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-38">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj38" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj38" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-39">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj19" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj39" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-40">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj40" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj40" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-41">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj41" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p41")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj41" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-42">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj42" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p42")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj42" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>








                                       <asp:TemplateField HeaderText="Project-43">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj43" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p43")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj43" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-44">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj44" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p44")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj44" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-45">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj45" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p45")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj45" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-46">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj46" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p46")).ToString("#,##000;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj46" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-47">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprj47" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p47")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFprj47" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-48">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj48" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p48")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj48" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-49">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj49" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p49")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj49" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project-50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj50" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p50")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj50" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-51">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj51" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p51")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj51" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Project-52">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj52" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p52")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj52" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-53">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj53" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p53")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj53" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-54">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj54" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p54")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj54" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                
                                 <asp:TemplateField HeaderText="Project-55">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj55" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p55")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj55" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-56">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj56" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p56")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj56" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-57">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj57" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p57")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj57" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-58">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj58" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p58")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj58" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-59">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj59" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p59")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj59" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-60">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj60" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p60")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj60" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-61">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj61" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p61")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj61" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-62">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj62" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p62")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj62" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>





                                       <asp:TemplateField HeaderText="Project-63">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj63" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p63")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj63" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-64">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj64" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p64")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj64" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-65">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj65" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p65")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj65" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-66">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj66" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p66")).ToString("#,##000;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj66" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-67">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprj67" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p67")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFprj67" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-68">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj68" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p68")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj68" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-69">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj69" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p69")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj69" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project-70">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj70" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p70")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj70" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-71">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj71" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p71")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj71" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Project-72">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj72" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p72")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj72" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-73">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj73" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p73")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj73" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-74">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj74" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p74")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj74" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                
                                 <asp:TemplateField HeaderText="Project-75">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj75" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p75")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj75" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-76">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj76" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p76")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj76" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-77">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj77" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p77")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj77" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-78">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj78" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p78")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj78" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-79">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj79" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p79")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj79" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-80">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj80" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p80")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj80" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-81">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj81" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p81")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj81" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-82">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj82" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p82")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj82" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>



                                       <asp:TemplateField HeaderText="Project-83">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj83" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p83")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj83" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-84">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj84" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p84")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj84" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-85">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj85" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p85")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj85" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-86">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj86" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p86")).ToString("#,##000;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj86" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-87">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprj87" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p87")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFprj87" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-88">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj88" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p88")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj88" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project-89">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj89" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p89")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj89" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Project-90">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj90" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p90")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj90" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-91">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj91" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p91")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj91" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Project-92">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj92" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p92")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj92" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-93">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj93" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p93")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj93" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-94">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj94" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p94")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj94" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                
                                 <asp:TemplateField HeaderText="Project-95">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj95" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p95")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj95" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-96">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj96" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p96")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj96" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-97">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj97" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p97")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj97" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-98">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj98" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p98")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj98" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project-99">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj99" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p99")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj99" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project-100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj100" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p100")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPrj100" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
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

