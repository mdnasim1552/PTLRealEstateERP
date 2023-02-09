
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BillEntry.aspx.cs" Inherits="RealERPWEB.F_16_Bill.BillEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

           <%-- try {
                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvRptResBasis.ClientID%>')
                    .gridviewScroll({
                        width: 1165,
                        height: 420,
                        arrowsize: 30,
                        railsize: 16,
                        barsize: 8,
                        headerrowcount: 2,
                        varrowtopimg: "../Image/arrowvt.png",
                        varrowbottomimg: "../Image/arrowvb.png",
                        harrowleftimg: "../Image/arrowhl.png",
                        harrowrightimg: "../Image/arrowhr.png",
                        freezesize: 5


                    });
            }

            catch (e) {

                alert(e.message);

            }--%>

        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt  lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>


                                    <div class="col-md-5  pading5px">

                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblProjectDesc" runat="server" Visible="False" CssClass=" form-control inputlblVal"></asp:Label>


                                    </div>
                                    <div class="col-md-1 pading5px ">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" Text="Ok"></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3  pading5px asitCol3 pull-right">
                                        <%--<asp:CheckBox ID="chkWithout" runat="server" TabIndex="10" Text="Without Details" CssClass="btn btn-primary checkBox" />--%>
                                        <asp:LinkButton ID="lbtnPreList" runat="server" CssClass=" smLbl_to" OnClick="lbtnPreList_Click">Prev.  List:</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevList" runat="server" Style="width: 120px" CssClass=" ddlPage lblTxt">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-8 pading5px">

                                        <asp:Label ID="lblvounotext" runat="server" CssClass="  lblName lblTxt" Text="Bill No:"></asp:Label>

                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputTxt inputBox50px"
                                            Text="BIL"></asp:Label>

                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="R/A No:"></asp:Label>
                                        <asp:DropDownList ID="ddlRA" runat="server" AutoPostBack="True" Style="width: 89px;" CssClass="ddlPage" OnSelectedIndexChanged="ddlRA_SelectedIndexChanged" TabIndex="18">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtRefno" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                        <asp:Label ID="lbldate" runat="server" CssClass=" smLbl_to " Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal1" runat="server" Format="dd-MMM-yyyy "
                                            TargetControlID="txtDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblpage" runat="server" CssClass=" smLbl_to" Text="Page Size:" Visible="False"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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



                                    </div>



                                </div>
                            </div>
                        </fieldset>

                        <asp:GridView ID="gvRptResBasis" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                            ShowFooter="True" Width="752px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvRptResBasis_RowCreated" OnRowDataBound="gvRptResBasis_RowDataBound">
                            <PagerSettings PageButtonCount="20" Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>sss</HeaderTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Catagory">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) +  Convert.ToString(DataBinder.Eval(Container.DataItem, "misirdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Description">

                                    <ItemTemplate>

                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))   %>'
                                            Width="230px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lnktotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbgdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkfinalup" runat="server" CssClass="btn btn-danger btn-xs" OnClick="lnkfinalup_Click">Update</asp:LinkButton>
                                    </FooterTemplate>


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillrate" runat="server" BackColor="Transparent" BorderStyle="None" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFordam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblordam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Executed Qty ">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvexeqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>







                                <asp:TemplateField HeaderText="Previous Quantity ">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprebqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prebqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText=" System Qty ">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sysqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Work Qty">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvwrkqty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="%">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpercnt" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Submitted Qty">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvproqty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Billing Qty">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkputsubmit" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lnkputsubmit_Click">Submit</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbillqty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbillam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblbillam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MB Number">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvmajbook" runat="server" BorderStyle="None" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbnumber")) %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Page Number">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpagenumber" runat="server" BorderStyle="None" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pagenumber")) %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>








                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>




                        <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                            <fieldset class=" scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">

                                        <div class="col-md-12  pading5px">
                                            <asp:Label ID="lblsecurity" runat="server" CssClass="lblTxt lblName" Text="SD in %"></asp:Label>
                                            
                                            <asp:TextBox ID="txtpercentage" runat="server" CssClass="inputtextbox" Style="width: 40px; text-align: right;" ></asp:TextBox>
                                            <asp:Label ID="lblsecamt" runat="server"
                                                Text="SD in Amt." CssClass=" smLbl_to"></asp:Label>

                                            <asp:TextBox ID="txtSDAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>

                                            <asp:Label ID="lbltaxper" runat="server" Text="Tax in %" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txttaxpercentage" runat="server" CssClass="inputtextbox" Style="width: 40px; text-align: right;" ></asp:TextBox>

                                            <asp:Label ID="lblTax" runat="server"
                                                Text="Tax in Amt." CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>


                                            <asp:Label ID="lblvatinper" runat="server"
                                                Text="Vat In %" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtvatpercentage" runat="server" CssClass="inputtextbox" Style="width: 40px; text-align: right;" ></asp:TextBox>


                                            <asp:Label ID="lblvat" runat="server"
                                                Text="Vat In Amt." CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtvatAmount" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>

                                            <asp:Label ID="lblAdvPer" runat="server"
                                                Text="Adv In %" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="TxtAdvPer" runat="server" CssClass="inputtextbox" Style="width: 40px; text-align: right;" Text="%"></asp:TextBox>

                                            <asp:Label ID="lblAdvanced" runat="server"
                                                Text="Advanced" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtAdvanced" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>

                                            <asp:Label ID="lblDeduc" runat="server"
                                                Text="Deduction" CssClass=" smLbl_to"></asp:Label>
                                            <asp:TextBox ID="txtDeduc" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>



                                            <asp:Label ID="lblnettotal" runat="server"
                                                Text="Net Total:" CssClass=" smLbl_to"></asp:Label>

                                            <asp:Label ID="lblvalnettotal" runat="server" CssClass="smLbl_to" Style="text-align: right; color: blue;"></asp:Label>


                                            <asp:LinkButton ID="lbtnDepost" runat="server" CssClass="btn btn-primary  btn-xs" OnClick="lbtnDepost_Click">Cal.</asp:LinkButton>


                                        </div>

                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-12  pading5px">
                                            <asp:Label ID="lblremarks" runat="server"
                                                Text="Remarks" CssClass="lblTxt lblName"></asp:Label>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="inputtextbox" TextMode="MultiLine" Style="height: 50px; width: 572px;"></asp:TextBox>

                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Sales  Team</asp:Label>
                                            <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="chzn-select ddlPage margin5px" TabIndex="12" Style="width: 200px; float: left">
                                            </asp:DropDownList>

                                        </div>




                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                    </div>


                                </div>


                            </fieldset>

                        </asp:Panel>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

