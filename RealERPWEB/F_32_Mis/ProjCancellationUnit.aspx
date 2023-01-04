<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjCancellationUnit.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjCancellationUnit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            var gv = $('#<%=this.gvPrjCancellation.ClientID %>');
            gv.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });


            $('#<%=this.chkasondate.ClientID%>').change(function () {

                try {
                    var chk = $(this).is(':checked');
                    if (chk == true) {
                        alert("ok");
                        $('#<%=this.txttoDate.ClientID%>').hide();
                        $('#<%=this.lbltodate.ClientID%>').hide();
                        $('#<%=this.lblfromDate.ClientID%>').text("Date:");
                    }
                    else {
                        alert("ok_11");
                        $('#<%=this.txttoDate.ClientID%>').show();
                        $('#<%=this.lbltodate.ClientID%>').show();
                        $('#<%=this.lbltodate.ClientID%>').text("To:");
                        $('#<%=this.lblfromDate.ClientID%>').text("From:");

                    }
                }
                catch (e) {

                    alert(e.message);

                }

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
                                    <div class="col-md-5 pading5px ">
                                        <%--<asp:Label ID="lblfromDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>--%>
                                        <asp:Label ID="lblfromDate" runat="server" CssClass="smLbl_to" Text="From:"></asp:Label>
                                        <asp:TextBox ID="txtfromDate" runat="server" CssClass=" inputtextbox" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calfromdate" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtfromDate"></cc1:CalendarExtender>

                                       
                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttoDate" runat="server" CssClass=" inputtextbox" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Caltodate" runat="server" Format="dd-MMM-yyyy " TargetControlID="txttoDate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                                    </div>
                                </div>




                                <div class="form-group">
                                    <div class="col-md-3 pading5px  asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProjind" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass=" chzn-select form-control inputTxt" TabIndex="3"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="chkasondate" runat="server" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label runat="server" Text="as on date" ID="lblnetbalance" CssClass="control-label"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <div class=" table table-responsive">

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvPrjCancellation" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="800px" OnRowDataBound="gvPrjCancellation_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right" Font-Size="12px"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField HeaderText="P-ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpnAmt" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjcode")) %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Project Name">

                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Project Name" Width="100px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="100px">Export Exel</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvkPrjName" runat="server"
                                                    Font-Size="12px" Font-Underline="False"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjname")) %>'
                                                    Width="210px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvcustomername" runat="server" Target="_blank" Style="text-align: left" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>' Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpayable" runat="server" Style="text-align: right" Font-Size="12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payable")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpayment" runat="server" Style="text-align: right" Font-Size="12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpay" runat="server" Style="text-align: right" Font-Size="12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

