<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurApproveEntry02.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurApproveEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            //            var gv1 = $('#<%=this.dgv1.ClientID %>');


            //            gv1.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled" CssClass="  inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblmrfno" runat="server" CssClass="smLbl_to text-left" Text="Aprov. No."></asp:Label>
                                        <asp:TextBox ID="txtserchmrf" runat="server" CssClass="ddlPage62" Text="PAP00-"></asp:TextBox>
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn margin5px"></asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Panel ID="pnlGridPage" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnFirst" runat="server" Height="18px" ImageUrl="~/Image/First.png"
                                                            OnClick="imgBtnFirst_Click" ToolTip="First Page" TabIndex="4" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnNext" runat="server" Height="18px" ImageUrl="~/Image/Next.png"
                                                            OnClick="imgBtnNext_Click" ToolTip="Next" TabIndex="5" />
                                                    </td>
                                                    <td class="style95">
                                                        <asp:Label ID="lblCurPage" runat="server" BackColor="White" ForeColor="Black" Height="18px"
                                                            Style="text-align: center" Width="30px" TabIndex="6">1</asp:Label>
                                                    </td>
                                                    <td class="style91">
                                                        <asp:ImageButton ID="imgBtnPerv" runat="server" Height="18px" ImageUrl="~/Image/Prev.png"
                                                            OnClick="imgBtnPerv_Click" ToolTip="Previous" TabIndex="7" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnLast" runat="server" Height="18px" ImageUrl="~/Image/Last.png"
                                                            OnClick="imgBtnLast_Click" ToolTip="Last Page" TabIndex="8" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ImgbtnFindProjectName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjectName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblReqno" runat="server" CssClass="lblTxt lblName" Text="Req No"></asp:Label>
                                        <asp:TextBox ID="txtsrchreqno" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ImgbtnFindSechreqno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSechreqno_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlReqno" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgv1_RowDataBound"
                        ShowFooter="True" Width="963px" PageSize="100" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowCancelingEdit="dgv1_RowCancelingEdit" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requistion No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvreqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvproject" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Requistion No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvreqno01" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MRF No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmrfno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requisition Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvreqdate" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description of Materials">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResDesc" runat="server"
                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                        Width="150px">
                                                            
                                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Balance Qty">

                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" Font-Size="14px"
                                        ForeColor="White" OnClick="lbtnResFooterTotal_Click" Style="text-align: center; height: 17px;"
                                        Width="75px">Total :</asp:LinkButton>
                                </FooterTemplate>


                                <ItemTemplate>
                                    <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="55px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New Order Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvNewOrderQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved Rate(Management)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmgtaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maprovrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Approved Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvNewApprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>

                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New Order Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvNewOrderAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFooterTAprovAmt" runat="server" Width="60px" ForeColor="White"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSuplDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc").ToString() %>'
                                        Width="125px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSupname" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Survey Link">

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                <ItemTemplate>


                                    <asp:HyperLink ID="hlnkgvSurvey" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno"))%>'
                                        Width="100px">
                                    </asp:HyperLink>


                                </ItemTemplate>



                                <FooterStyle HorizontalAlign="left" />
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvmrno" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                        Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok" OnClick="lbok_Click"
                                        Width="30px">OK</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="ResCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrsircode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SupplierCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvsupliercode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ssircode").ToString() %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
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




            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
