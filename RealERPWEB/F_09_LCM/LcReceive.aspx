
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LcReceive.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LcReceive" %>

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
                                    <div class="col-md-1 pading5px">

                                        <asp:Label ID="lblLcno" runat="server" CssClass="lblTxt lblName">L/C Number</asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlLcCode" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="LbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="LbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                    

                                </div>
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px">
                                            <asp:Label ID="lblreceivedat" runat="server" CssClass="lblTxt lblName">Receive Date:</asp:Label>
                                            <asp:TextBox ID="txtreceivedate" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalExr2" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtreceivedate" />
                                            <asp:Label ID="lblgrr" runat="server" CssClass="lblTxt lblName">LRC No.</asp:Label>
                                            <asp:TextBox ID="txtgrrno" runat="server" CssClass=" inputtextbox" ReadOnly="True" TabIndex="14" Width="100px"></asp:TextBox>
                                            <asp:CheckBox ID="chkExel" runat="server" AutoPostBack="True" CssClass="btn btn-warning primaryBtn checkBox" OnCheckedChanged="chkExel_CheckedChanged" TabIndex="9" Text="Input From Exel?" />
                                            <asp:Panel ID="pnlExel" runat="server" Style="margin-top: -5px;" TabIndex="22" Visible="False">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exel File"></asp:Label>
                                                        <div class="uploadFile">
                                                            <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblstorid" runat="server" CssClass="lblTxt lblName">Store Id:</asp:Label>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlStorid" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select" TabIndex="16">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="lnkReceive" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkReceive_Click">Receive</asp:LinkButton>
                                        </div>
                                    </div>
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
                                            <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
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
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                            <asp:TemplateField HeaderText="Free Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgrvFreeqty1" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFFreeqty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec. Upto Last" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvreuptlast" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvuptolast")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                            <asp:TemplateField HeaderText="Remaining Order" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrmainord" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainordr")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                            <asp:TemplateField HeaderText="Receive Qty" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvrcvQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                                        Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                                        BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
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
                                            <asp:TemplateField HeaderText="Expeire Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtexpeirdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy ") %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtexpeirdate"></cc1:CalendarExtender>
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


                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <asp:Panel ID="pnlexcelheading" runat="server" Visible="False" TabIndex="22">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblheding" runat="server" CssClass=" dataLblview" Text="Product Details"></asp:Label>

                                                    </div>

                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </fieldset>


                                    <asp:Repeater ID="rpprodetails" runat="server">

                                        <HeaderTemplate>
                                            <table id="tblrpprodetails" class=" table-striped table-hover table-bordered grvContentarea">
                                                <tr>
                                                    <th>SL</th>
                                                    <th>Product_Id</th>
                                                    <th>Pack_No</th>
                                                    <th>M_IMEI</th>
                                                    <th>S_IMEI</th>
                                                    <th>Serial_No</th>
                                                    <th>Color</th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lrpproid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Product_Id")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblrppackno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Pack_No")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpmimei" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "M_IMEI")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpsimei" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "S_IMEI")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpselno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Serial_No")) %>'
                                                        Width="110px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpColor" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Color")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>

                                            </tr>

                                        </ItemTemplate>

                                        <FooterTemplate>

                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>

                                                <th></th>
                                            </tr>


                                            </table>
                                        </FooterTemplate>





                                    </asp:Repeater>
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



