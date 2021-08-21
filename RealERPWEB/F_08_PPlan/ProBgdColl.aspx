
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProBgdColl.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ProBgdColl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var dgvAccRec02 = $('#<%=this.gvAnalysis.ClientID %>');

            dgvAccRec02.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 10,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 4
            });

            var gvAnalysis = $('#<%=this.gvAnalysis.ClientID%>');
            gvAnalysis.Scrollable();

        }

    </script>


    <style type="text/css">
        .style101 {
            border-style: none;
        }
    </style>






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

                                        <asp:Label ID="lblItem" runat="server" Font-Size="11px" CssClass=" lblName lblTxt">Project Name:</asp:Label>

                                        <asp:TextBox ID="txtItemSearch" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblItemDesc" runat="server" Visible="false" CssClass="form-control inputTxt"></asp:Label>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk1_Click">Ok</asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pull-right">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . . . ."
                                                    Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>

                            </div>
                        </fieldset>


                        <asp:Panel ID="PnlAnalysis" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-7 pading5px">
                                            <asp:Label ID="lbl01" runat="server" CssClass="lblTxt lblName">Start Date</asp:Label>
                                            <asp:TextBox ID="lblStartDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="lbl2" runat="server" CssClass=" smLbl_to">End Date</asp:Label>
                                            <asp:TextBox ID="lblEndDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            <asp:Label ID="lblduration" runat="server" CssClass=" smLbl_to"></asp:Label>

                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to">Page</asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage" Style="width: 75px;">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                            </asp:DropDownList>


                                        </div>
                                        <div class="col-md-4 pull-right">
                                            <asp:LinkButton ID="lbtnInputSame" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnInputSame_Click">Put same value for all Month</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnUpdateAna" runat="server" class="btn btn-danger primaryBtn" OnClick="lbtnUpdateAna_Click" Style="margin-left: 5px;">Update</asp:LinkButton>

                                        </div>
                                        <asp:Label ID="lblmsg" runat="server" CssClass=" btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                        <div class="clearfix"></div>

                                    </div>

                                    <div class="form-group">
                                    </div>
                                </div>
                            </fieldset>


                            <asp:GridView ID="gvAnalysis" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="HeaderStyle"
                                Width="16px" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                                AllowPaging="True" OnRowDataBound="gvAnalysis_RowDataBound">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtngvSlNo" runat="server" Font-Bold="True"
                                                Font-Size="12px" Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res. Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Resources">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnAutoGenerate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAutoGenerate_Click">Auto Generate</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc" runat="server" BackColor="Transparent" BorderStyle="None" ForeColor="Black" Target="_blank"
                                                Font-Size="11px" Style="text-align: left" Width="350px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'> </asp:HyperLink>
                                            <%--<asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="350px"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget Amt">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBudget" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="txtBudget" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAlBudget" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "albgdam")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAlBudget" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Init.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty001" runat="server" Style="text-align: right; border-style: none;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty001")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobiliz." Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty002" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty002")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubStruc." Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty003" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty003")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-1" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty004" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty004")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-2" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty005" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty005")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-3" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty006" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty006")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Base-3" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty007" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty007")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Base-3" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty008" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty008")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-3" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty009" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty009")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty010" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty010")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty011" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty011")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="3rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty012" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty012")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="4th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty013" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty013")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="5th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty014" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty014")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="6th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty015" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty015")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="7th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty016" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty016")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="8th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty017" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty017")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="9th floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty018" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty018")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="10th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty019" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty019")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="11th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty020" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty020")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="12th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty021" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty021")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="13th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty022" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty022")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="14th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty023" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty023")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="15th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty024" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty024")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="16th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty025" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty025")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="17th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty026" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty026")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="18th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty027" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty027")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="19th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty028" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty028")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="20th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty029" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty029")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="21st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty030" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty030")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="22ndFloor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty031" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty031")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="23rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty032" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty032")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="24th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty033" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty033")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="25th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty034" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty034")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="26th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty035" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty035")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="27th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty036" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty036")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="28th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty037" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty037")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty038" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty038")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty039" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty039")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty040" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty040")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty041" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty041")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty042" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty042")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty043" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty043")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty044" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty044")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty045" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty045")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty046" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty046")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty047" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty047")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty048" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty048")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty049" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty049")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty050" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty050")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty051" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty051")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty052" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty052")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty053" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty053")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty054" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty054")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty055" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty055")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty056" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty056")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty057" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty057")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty058" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty058")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty059" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty059")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty060" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty060")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty061" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty061")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty062" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty062")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty063" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty063")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty064" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty064")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty065" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty065")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty066" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty066")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty067" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty067")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty068" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty068")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty069" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty069")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty070" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty070")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty071" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty071")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty072" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty072")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty073" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty073")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty074" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty074")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty075" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty075")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty076" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty076")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty077" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty077")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty078" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty078")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty079" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty079")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty080" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty080")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty081" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty081")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty082" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty082")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty083" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty083")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty084" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty084")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty085" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty085")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty086" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty086")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty087" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty087")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty088" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty088")).ToString("#,##0;-#,##0; ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>



                            <%-- <table style="width: 924px; margin-right: 0px">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style37">
                                                <asp:Label ID="lblItem5" runat="server" CssClass="newStyle3" Font-Size="12px"
                                                    Font-Underline="True" Style="font-weight: 700" Text="Description of Resources"
                                                    Width="320px"></asp:Label>
                                            </td>
                                            <td class="style36">
                                               
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="style107">
                                                
                                            </td>
                                            <td class="style119">&nbsp;</td>
                                            <td class="style43">&nbsp;</td>
                                            <td class="style17">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style114">&nbsp;</td>
                                            <td class="style115">&nbsp;</td>
                                            <td class="style116">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">
                                                <asp:TextBox ID="txtResSearch" runat="server" AutoCompleteType="Disabled"
                                                    BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="66px"></asp:TextBox>
                                            </td>
                                            <td class="style34">
                                                <asp:ImageButton ID="ImgbtnFindResource" runat="server" Height="19px"
                                                    ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindResource_Click"
                                                    Width="16px" />
                                            </td>
                                            <td class="style37">
                                                <asp:DropDownList ID="ddlResource" runat="server" CssClass="newStyle1"
                                                    Font-Bold="True" Font-Size="11px" Height="20px" Width="327px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style36">
                                                <asp:LinkButton ID="lbtnOk2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    OnClick="lbtnOk2_Click" Style="text-align: center" Width="67px">Select Res.</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="style107">
                                                <asp:Label ID="lblItem6" runat="server" CssClass="newStyle3" Font-Size="12px"
                                                    Font-Underline="False" Style="font-weight: 700" Text="Current Column Group:"
                                                    Width="131px"></asp:Label>
                                            </td>
                                            <td class="style119">
                                                <asp:Label ID="lblColGroup" runat="server" BackColor="#660033" Font-Size="25px"
                                                    ForeColor="White" Style="text-align: center; font-weight: 700"
                                                    Width="24px">1</asp:Label>
                                            </td>
                                            
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33" colspan="16">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style37">
                                                
                                            </td>
                                            <td class="style36">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style119">&nbsp;</td>
                                            <td class="style43">&nbsp;</td>
                                            <td class="style17">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style114">&nbsp;</td>
                                            <td class="style115">&nbsp;</td>
                                            <td class="style116">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                    </table>--%>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

