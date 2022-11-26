<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurReqApproval.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurReqApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=this.txtserchmrf.ClientID%>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);
            $('.chzn-select').chosen({ search_contains: true });
            //$('#<%=this.gvsupres.ClientID%>').tblScrollable();
        }
        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
        }
        function openSupModal() {
            $('#modalSupRate').modal('toggle');
        }

        function closeSupModal() {
            $('#modalSupRate').modal('hide');
        }

        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
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
                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputTxt inputName inpPixedWidth"
                                            TabIndex="9"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProjectName0" CssClass="btn btn-primary srearchBtn"
                                                runat="server" OnClick="ImgbtnFindProjectName_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlProject" runat="server" Style="width: 180px" CssClass="chzn-select form-control inputTxt"
                                            TabIndex="10">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click" CausesValidation="false" EnableViewState="true"
                                            TabIndex="3">Ok</asp:LinkButton>

                                        <asp:Panel ID="pnlGridPage" runat="server" Visible="false" CssClass="pagination paginationPart">
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
                                    <div class="col-md-3 pading5px asitCol3 pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn-sm btn disabled" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="true"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-9 pading5px">
                                        <asp:Label ID="lblmrfno" runat="server" CssClass=" smLbl_to" Text=""></asp:Label>
                                        <asp:TextBox ID="txtserchmrf" runat="server" CssClass=" inputtextbox" Style="width: 83px;"
                                            TabIndex="2"></asp:TextBox>


                                    </div>
                                </div>

                                <%--   style="display: none;"--%>

                                <div class="form-group" style="display: none;">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblReqno" runat="server" CssClass="lblTxt lblName" Text="Req No:"></asp:Label>
                                        <asp:TextBox ID="txtsrchreqno" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindSechreqno" CssClass="btn btn-primary srearchBtn"
                                                runat="server" OnClick="ImgbtnFindSechreqno_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:DropDownList ID="ddlReqno" runat="server" CssClass="form-control inputTxt"
                                            TabIndex="10">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                            </div>
                        </fieldset>

                        <div class="table-responsive">
                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgv1_RowDataBound"
                                ShowFooter="True" PageSize="100"
                                OnRowCancelingEdit="dgv1_RowCancelingEdit" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requistion No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grp SL" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgpsl" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gpsl")) %>'
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
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requistion No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqno01" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrfno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqdate" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblgvResDesc" runat="server" Target="_blank"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))
                                                                            %>'
                                                Width="100px">
                                                            
                                                            
                                            </asp:HyperLink>
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
                                    <asp:TemplateField HeaderText="Present Require </br>ment">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnResFooterTotal" runat="server" OnClick="lbtnResFooterTotal_Click"
                                                CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvReqQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approv. Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Purchase Rate">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate" runat="server" OnClientClick="return Confirmation();" OnClick="lbtnFinalUpdate_Click" CssClass="btn btn-danger primarygrdBtn">Update </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgvlpurRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budget Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvboqRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approved Rate(Mgt)" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmgtaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mreqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="New Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsupRat" runat="server" BorderColor="#99CCFF" BackColor="White" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqsrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:TextBox>


                                        </ItemTemplate>
                                        <HeaderStyle ForeColor="Blue" />
                                        <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dis </br>count">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdispercnt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt"))>0?"%":"") %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Actual</br>Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Budget Amount" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdreqamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdreqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFbgdreqamt" runat="server" ForeColor="White" Width="60px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Amount" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTAprAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFooterTAprAmt" runat="server" ForeColor="White" Width="60px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvmrno" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                Width="15px" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAll_CheckedChanged" Width="15px" />

                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok" OnClientClick="return Confirmation();" OnClick="lbok_Click"
                                                Width="35px" Text="Update">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">

                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlSupname" CssClass=" ddlPage125 chzn-select" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </ItemTemplate>

                                        <HeaderTemplate>
                                            <p>Supplier's</p>

                                            <asp:CheckBox ID="chkSameSupplier" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkSameSupplier_CheckedChanged" Width="15px" />
                                            Same Supplier
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Specification">
                                        <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click"  CssClass="btn btn-danger primarygrdBtn">Final Update </asp:LinkButton>
                                    </FooterTemplate>--%>

                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlspcfdesc" CssClass=" ddlPage125 chzn-select" runat="server" Width="140px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment Type">
                                        <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click"  CssClass="btn btn-danger primarygrdBtn">Final Update </asp:LinkButton>
                                    </FooterTemplate>--%>

                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlptype" CssClass="ddlPage chzn-select" runat="server" Width="80px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
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
                                                Width="80px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:HyperLink Visible="false" ID="HypMakeSurvey" runat="server" Target="_blank">Survey</asp:HyperLink>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                                    <%--<asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkvmrno" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok" OnClick="lbok_Click"
                                            Width="30px">OK</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Store Name" Visible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlStorename" runat="server" Width="125px">
                                            </asp:DropDownList>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Site Supply Date /</br>delivery date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>' Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Supply Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpursupDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "pursdate").ToString() %>' Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>' Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Store Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstorecode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "storecode").ToString() %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
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

                                    <asp:TemplateField HeaderText="ptype" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvptype" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ptype").ToString() %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="History">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnHistory" runat="server" OnClick="lbtnHistory_Click"
                                                Width="50px" Text="Show">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                    <div class=" row table-responsive">
                        <asp:Panel ID="pnlnara" runat="server">
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblreqnaration" runat="server" class="lblTxt lblName" Width="900px" Text="Req Narration: " Font-Bold="true" Style="text-align: left"> </asp:Label>
                                        </span>

                                    </div>
                                </div>
                                <div class="col-md-4 pading5px">
                                    <div class="input-group">
                                        <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                            NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                            </div>
                        </asp:Panel>

                        <%--  Rahain to Request below panle visible false, by nahid--%>
                        <asp:Panel ID="fotpanel" Visible="false" runat="server">
                            <div class="col-md-12">
                                <div class="panel with-nav-tabs panel-deafult">
                                    <div class="panel-heading">
                                        <ul class="nav nav-tabs ">
                                            <li class="active"><a href="#tab3primary" data-toggle="tab"><span class="glyphicon glyphicon-upload"></span>Upload </a></li>
                                        </ul>
                                    </div>
                                    <div class="panel-body">
                                        <div class="tab-content">
                                            <div class="tab-pane fade in active" id="tab3primary">
                                                <div class="form-group">
                                                    <div class="col-md-4 col-sm-4 col-lg-4">
                                                        <asp:Panel runat="server" ID="pnlQutatt">
                                                            <div class="panel panel-primary">
                                                                <div class="panel-heading">
                                                                    <span class="glyphicon glyphicon-upload">Qutation Image Upload</span>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <div class="row">
                                                                                <div class="form-group">
                                                                                    <asp:Label ID="Label2" runat="server" CssClass="col-md-4" Text="Supplier Name"></asp:Label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:DropDownList ID="ddlBestSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <fieldset class="alert alert-success">

                                                                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                                        OnClientUploadComplete="uploadComplete" runat="server"
                                                                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                                        CompleteBackColor="White"
                                                                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                                        OnUploadedComplete="FileUploadComplete" />
                                                                                    <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                                                    <br />
                                                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                                                </fieldset>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <div class="panel panel-primary">
                                                            <div class="panel-heading">
                                                                <span class="glyphicon glyphicon-picture">Uploaded Files</span>
                                                                <div class="pull-right">
                                                                    <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_Click" />
                                                                    <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body ">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                                            <LayoutTemplate>
                                                                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                                    <div id="EmpAll" runat="server">

                                                                                        <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                                                        <asp:Label ID="reqno" Visible="False" runat="server" Text='<%# Eval("reqno") %>'></asp:Label>
                                                                                        <asp:Label ID="sircode" Visible="False" runat="server" Text='<%# Eval("sircode") %>'></asp:Label>

                                                                                        <a href="../Upload/Purchase/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                                                            <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                                        </a>
                                                                                        <div class="checkboxcls">
                                                                                            <asp:CheckBox ID="ChDel" runat="server" />
                                                                                        </div>


                                                                                    </div>

                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>


                            </div>



                        </asp:Panel>
                    </div>
                    <asp:Panel runat="server" ID="pnlApproval" Visible="true">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblCarring" runat="server" CssClass="lblTxt lblName" Text="Carring"></asp:Label>
                                            <asp:TextBox ID="txtCarring" runat="server" CssClass=" inputTxt inputName inpPixedWidth" Style="text-align: right;"
                                                TabIndex="9"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>
                    <div class="table-responsive">
                        <div class=" form-group">
                            <asp:Label ID="lblsurveyby" Style="font-size: 16px; color: blue; margin: 10px 0" runat="server" Text="Approv.Date" Visible="False"></asp:Label>
                        </div>
                        <asp:GridView ID="gvMSRInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="274px" Visible="false">
                            <PagerSettings />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Materials Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRResUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSuplDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concern  Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRCperson" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Telephone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRPhone" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRMobile" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRRate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvbrand" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                            Height="16px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRDel" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delivery")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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

            <div id="modalSupRate" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-md-width">
                    <div class="modal-content modal-content-md-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Material Purchase History </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                        </div>
                        <div class="modal-body " style="min-height: 400px">
                            <div class="container">
                                <div class="row">
                                    <div class="form-group">
                                        <h4 class="modal-title"><span id="spanMatName" runat="server"></span></h4>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <asp:GridView ID="gvsupres" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" Font-Size="12px" Width="560px">
                                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="#000" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mlblpactdesc" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MPR/MRF">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mlblmrfno" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mlblssirdesc" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mtxtgvlimit" runat="server" Font-Size="11px" Style="text-align: right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
