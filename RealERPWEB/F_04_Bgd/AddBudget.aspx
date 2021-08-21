<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AddBudget.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.AddBudget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

         

            var dgvAddBgd = $('#<%=this.dgvAddBgd.ClientID %>');

            dgvAddBgd.gridviewScroll({
                width: 1000,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });


            // Key Navigation
            $.keynavigation(dgvAddBgd);
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
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-6  pading5px ">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="AB Date:"></asp:Label>

                                            <asp:TextBox ID="txtABDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtABDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtABDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="AB No."></asp:Label>

                                            <asp:Label ID="lblCurABNo1" runat="server" CssClass="inputtextbox" Text="AB00-"></asp:Label>

                                            <asp:TextBox ID="txtCurABNo2" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        </div>

                                        <div class="col-md-4  pading5px pull-right ">

                                            <asp:LinkButton ID="imgbtnFindPreAb" runat="server"  CssClass="lblTxt lblName"   OnClick="imgbtnFindPreAb_Click" Text="Addi. Budget No."></asp:LinkButton>                                             

                                            <asp:DropDownList ID="ddlPrevABList" runat="server" Width="150" CssClass="ddlPage "></asp:DropDownList>

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-5   pading5px">

                                            <asp:Label ID="Label9" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSearchPrj" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindPrj" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindPrj_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>



                                            <asp:DropDownList ID="ddlProjectName" runat="server" Style="width: 310px;" CssClass="ddlPage chzn-select"></asp:DropDownList>

                                            <asp:Label ID="lblProjectName" runat="server" CssClass="inputtextbox" Visible="False" Width="310px"></asp:Label>


                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary  btn-xs" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                                        </div>


                                    </div>

                                 

                                </asp:Panel>

                              
                            </div>
                        </fieldset>


                           <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                          <asp:Panel ID="pnlItem" Visible="false" runat="server">

                                   <div class="form-group">
                                        <div class="col-md-5   pading5px ">
                                            <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Page Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5" CssClass="ddlPage">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>


                                            <asp:DropDownList ID="ddlItem" runat="server" Style="width: 310px" CssClass="chzn-select ddlPage  inputTxt" TabIndex="3">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>


                                         <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnselect" runat="server" CssClass="btn btn-primary  btn-xs" OnClick="lbtnselect_Click">Select</asp:LinkButton>
                                        </div>

                                    </div>
                                    </asp:Panel>
                                </div>
                               </fieldset>


                        <asp:GridView ID="dgvAddBgd" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" AllowPaging="True"
                            OnPageIndexChanging="dgvAddBgd_PageIndexChanging" Width="988px">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNor" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Isircode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcodedep" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateAdBgd" runat="server" OnClick="lbtnUpdateAdBgd_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>



                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:LinkButton ID="lbtnApproved" runat="server" OnClick="lbtnApproved_Click" CssClass="btn btn-primary  primaryBtn">Approved</asp:LinkButton>


                                    </FooterTemplate>


                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit"))%>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True"
                                            Font-Size="13px" ForeColor="#000"
                                            Style="text-align: right; height: 15px;" Width="40"
                                            OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bgd Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBgdQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bgd Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBgdRat" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bgd Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBgdAmt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbgvFBgdAmt" runat="server" Font-Bold="True"
                                            Font-Size="13px" ForeColor="#000"
                                            Style="text-align: right; height: 15px;" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>









                                <asp:TemplateField HeaderText="Proposed Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProQty" runat="server" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Proposed Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAprAmt" runat="server" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbgvFApramt" runat="server" Font-Bold="True"
                                            Font-Size="13px" ForeColor="#000"
                                            Style="text-align: right; height: 15px;" Width="80"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>





                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>

                      <div class="row">
                         <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                       <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                    </div>
                                    </div>

                            </fieldset>
                         </asp:Panel>
                      </div>


                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



