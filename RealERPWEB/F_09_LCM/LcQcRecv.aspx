<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LcQcRecv.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LcQcRecv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
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
                        <fieldset class="scheduler-border fieldset_B">




                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblLcno" runat="server" CssClass="smLbl_to">L/C </asp:Label>

                                        <asp:DropDownList ID="ddlLcCode" runat="server" Width="270px" OnSelectedIndexChanged="ddlLcCode_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">Rec Number</asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlrcvno" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="LbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="LbtnOk_Click">Ok</asp:LinkButton>
                                        </div>

                                        <asp:Label ID="lblreceivedat" runat="server" CssClass="smLbl_to" Style="margin-left: 5px;"> QC Date:</asp:Label>
                                        <asp:TextBox ID="txtreceivedate" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalExr2" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtreceivedate" />
                                        <asp:Label ID="lblgrr" runat="server" CssClass="smLbl_to">GRN No.</asp:Label>
                                        <asp:TextBox ID="txtgrrno" runat="server" CssClass=" inputtextbox" ReadOnly="True" TabIndex="14" Width="100px"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <div class="msgHandSt">


                                            <%--    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                                    Text="Please Wait.........."></asp:Label>

                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                                        </div>

                                    </div>

                                </div>
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-group" style="display: none">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPreGrn" runat="server" CssClass="lblTxt lblName">Pre GRN:</asp:Label>
                                            <asp:TextBox ID="txtsrGrn" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnPreGrn" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnPreGrn_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlPreGrn" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select" TabIndex="16">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </fieldset>

                                <div class="col=-md-12">
                                    <asp:GridView ID="dgvReceive" runat="server" AllowPaging="false"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="dgvReceive_RowDataBound">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode1" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRcvdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy ") %>'></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbnTotal" Font-Bold="true" runat="server">Total</asp:Label>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcdesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvUnit1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFordqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="QC Upto Last" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvreuptlast" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFreuptlast" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remaining QC" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrmainord" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFrmainord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QC Qty" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvrcvQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                                        Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                                        BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFrcvQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot No." HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvlotno" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exp Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtexpeirdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy ") %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtexpeirdate"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remarks" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtremarks" runat="server" BackColor="White" BorderStyle="Solid" BorderColor="#00CCFF"
                                                        Width="150px" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                                   
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                        </Columns>


                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>


                                    <div class="clearfix"></div>
                                    <div>
                                        <asp:Label ID="lblPrintMsg" runat="server" CssClass="FormLevel"></asp:Label>

                                    </div>

                                </div>
                            </div>
                        </fieldset>



                    </div>

                </div>
                </fieldset>
                
                    <div class="row">
                    </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


