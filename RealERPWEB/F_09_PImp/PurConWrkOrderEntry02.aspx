<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurConWrkOrderEntry02.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurConWrkOrderEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <style type="text/css">
        .lineheight {
        }


        .headerbold {
            font-weight: bold;
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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Order No:"></asp:Label>
                                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>
                                        <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="inputTxt inputBox50px" TabIndex="3">000</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>


                                        <asp:Label ID="lblorderref" runat="server" CssClass="smLbl_to" Text="Reference"></asp:Label>

                                        <asp:TextBox ID="txtOrderRef" runat="server" CssClass="inputTxt  inputtextbox" TabIndex="3" Width="195px"></asp:TextBox>


                                        <div class="col-md-4 pading5px pull-right">
                                            <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevList_Click">Prev. List:</asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="chzn-select form-control  inputTxt" Width="250px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px ">
                                        <asp:Label ID="lblContractorList" runat="server" CssClass="lblTxt lblName" Text="Contractor Name"></asp:Label>
                                    </div>

                                    <div class="col-md-5 pading5px">

                                        <asp:DropDownList ID="ddlContractorlist" runat="server" CssClass="chzn-select form-control" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblddlContractor" runat="server" Visible="False" CssClass=" form-control"></asp:Label>

                                    </div>



                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass=" form-control"></asp:Label>


                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblduration" runat="server" CssClass="lblTxt lblName" Text="Duration"></asp:Label>
                                        <asp:TextBox ID="txtduration" runat="server" CssClass=" inputTxt inputtextbox" TabIndex="10"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblbillnature" runat="server" CssClass="lblTxt lblName" Text="Nature of Work"></asp:Label>
                                        <asp:DropDownList ID="ddlbilltype" runat="server"
                                            CssClass=" ddlPage chzn-select" Style="width: 282px;">
                                        </asp:DropDownList>



                                    </div>

                                </div>
                            </div>
                        </fieldset>


                        <asp:Panel ID="PnlRes" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="lblgroup" runat="server" CssClass="  lblTxt lblName" Text="Group"></asp:Label>

                                            <asp:DropDownList ID="ddlgroup" runat="server" CssClass="chzn-select  ddlPage" TabIndex="3" Style="width: 285px;" AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-md-1 pading5px">

                                            <asp:Label ID="lbllabour" runat="server" CssClass="  lblTxt lblName" Text="Labour"></asp:Label>
                                        </div>

                                        <div class="col-md-4 pading5px">

                                            <asp:DropDownList ID="ddllabour" runat="server" CssClass="chzn-select form-control" TabIndex="3"></asp:DropDownList>


                                        </div>
                                        <div class="col-md-3 pading5px ">

                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" OnClick="lbtnSelectAll_Click" CssClass="btn btn-primary primaryBtn"
                                                TabIndex="17">Select ALL</asp:LinkButton>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-primary primaryBtn"
                                                TabIndex="17" Style="margin-left: 5px;">Select</asp:LinkButton>



                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt smLbl_to" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Style="width: 50px;" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:CheckBox ID="chkCopy" runat="server" TabIndex="10" Text="Copy" CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkCopy_CheckedChanged" />


                                        </div>


                                    </div>





                                </div>

                            </fieldset>


                        </asp:Panel>


                        <asp:Panel ID="PnlCopy" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_B">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcopyorderno" runat="server" CssClass="lblTxt lblName">From Order</asp:Label>
                                            <asp:TextBox ID="txtScrchcopyorderno" runat="server" CssClass="inputtextbox" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnCopyorderno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnCopyorderno_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                        <div class="col-md-7 pading5px">
                                            <asp:DropDownList ID="ddlcopyorder" runat="server" CssClass=" ddlPage" TabIndex="3" Style="width: 200px;">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnCopyOrder" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyOrder_Click" TabIndex="4">Copy</asp:LinkButton>

                                        </div>



                                    </div>
                                </div>
                            </fieldset>

                        </asp:Panel>

                        <asp:GridView ID="gvorder" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Width="600px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvorder_PageIndexChanging" PageSize="15">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtndelete" runat="server" CssClass="btn btn-xs btn-danger" OnClick="lbtndelete_Click"><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtserial" runat="server"   
                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "serial")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">

                                    <FooterTemplate>

                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn  btn-primary  primarygrdBtn"
                                            OnClick="lbtnTotal_Click">Total</asp:LinkButton>


                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>

                                        <asp:Label ID="lblwrkdesc" runat="server" Font-Bold="true"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="480px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        <asp:TextBox ID="txtwrkdesc" BackColor="Transparent" BorderStyle="None" runat="server" TextMode="MultiLine" Height="50px"
                                            Text='<%#    Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim()
                                         
                                                                         
                                                                    %>'
                                            Width="565px">
                                            <%--sdetails--%>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Specification">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtspec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spec")) %>' Width="114px" BorderStyle="None" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Budgeted Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbgdqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty(Appx.)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvordqty" runat="server"   
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvordamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblFgvordamt" runat="server" Width="70px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisurmk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                        <asp:Panel ID="PnlNarration" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                     <div class="form-group">
                                        <div class="col-md-12 pading5px ">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="comncdate" runat="server" CssClass="lblTxt" Text="Date Of Commencement Of Work:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtcomncdat" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcomncdat"></cc1:CalendarExtender>
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="compltdate" runat="server" CssClass="lblTxt" Text="Date Of Completion:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtcompltdat" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcompltdat"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        
                                        <div class="col-md-12 pading5px " style="display: none;">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Subject:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt">Purchase Order For Materials</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px" style="display: none;">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Dear Sir,"></asp:Label>
                                                </span>

                                            </div>
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px" style="display: none;">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text=":"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtLETDES" runat="server" class="form-control inputTxt"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Term & Condition:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtTerm" runat="server" class="form-control" Rows="2" TextMode="MultiLine" Style="height: 210px; line-height: 18px;"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-3 pading5px">
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

