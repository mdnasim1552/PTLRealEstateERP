<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurConWrkOrderEntry02.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurConWrkOrderEntry02" %>

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
          .mt20 {
            margin-top: 20px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .chzn-container {
            width: 100% !important;
        }
                .chzn-container-single .chzn-search input{
            width:100%!important;
        }
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


            <div class="card mt-4">
                <div class="card-header">
                    <div class="form-group row">
                        <div class="col-lg-2 ">
                            <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Order No:"></asp:Label>
                            <div class="row">
                                <asp:Label ID="lblCurISSNo1" runat="server" CssClass="col-6 form-control form-control-sm"></asp:Label>
                                <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="col-6 form-control form-control-sm">000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1 ">
                            <asp:Label ID="Label7" runat="server" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-1 ">
                            <asp:Label ID="lblorderref" runat="server" Text="Reference"></asp:Label>
                            <asp:TextBox ID="txtOrderRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>

                        <div class="col-lg-2">
                            <asp:Label ID="Label1" runat="server">
                                Prev. List <asp:LinkButton ID="lbtnPrevList" runat="server" OnClick="lbtnPrevList_Click"><i class="fa fa search"></i></asp:LinkButton>
                            </asp:Label>
                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                        </div>




                        <div class="col-lg-2">
                            <asp:Label ID="lblContractorList" runat="server" Text="Contractor Name"></asp:Label>
                            <asp:DropDownList ID="ddlContractorlist" runat="server" CssClass="chzn-select form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>


                        <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>


                    </div>
                    <div class="form-group row">

                        <div class="col-lg-3">
                            <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>

                            <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                        </div>

                        <div class="col-lg-1 ">
                            <asp:Label ID="lblduration" runat="server" Text="Duration"></asp:Label>
                            <asp:TextBox ID="txtduration" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-lg-2 ">
                            <asp:Label ID="lblbillnature" runat="server" Text="Nature of Work"></asp:Label>
                            <asp:DropDownList ID="ddlbilltype" runat="server"
                                CssClass=" chzn-select form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 mt20">
                            <asp:CheckBox ID="checkletter" runat="server" Text="Offerletter Print" CssClass="mt20" AutoPostBack="True" />
                        </div>
                    </div>
                </div>




    
            <div class="card-body">

            <asp:Panel ID="PnlRes" runat="server" Visible="False">



                    <div class="form-group row">
                        <div class="col-lg-2 ">
                            <asp:Label ID="lblgroup" runat="server" Text="Group"></asp:Label>
                            <asp:DropDownList ID="ddlgroup" runat="server" CssClass="chzn-select  form-control form-control-sm"  AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"></asp:DropDownList>
                        </div>
             
                        <div class="col-lg-2 ">
                            <asp:Label ID="lbllabour" runat="server" Text="Labour"></asp:Label>
                            <asp:DropDownList ID="ddllabour" runat="server" CssClass="chzn-select form-control form-control-sm" ></asp:DropDownList>
                        </div>

                        <div class="col-lg-3">
                            <div class="btn-group">
                         <asp:LinkButton ID="lbtnSelectAll" runat="server" OnClick="lbtnSelectAll_Click" CssClass="btn btn-primary btn-sm mt20"
                                TabIndex="17">Select ALL</asp:LinkButton>
                            <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-primary btn-sm mt20 ml-1"
                                TabIndex="17" Style="margin-left: 5px;">Select</asp:LinkButton>
                            <asp:CheckBox ID="chkCopy" runat="server"  Text="Copy" CssClass="btn btn-primary btn-sm ml-1 mt20" AutoPostBack="True" OnCheckedChanged="chkCopy_CheckedChanged" />
                            </div>
                            </div>

                        <div class="col-lg-1">
        
                            <asp:Label ID="lblPage" runat="server"  Text="Page"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
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


            </asp:Panel>


            <asp:Panel ID="PnlCopy" runat="server" Visible="False">


                    <div class="form-group row">

                        <div class="col-lg-2 ">
                            <asp:Label ID="lblcopyorderno" runat="server" CssClass="lblTxt lblName">From Order
                            <asp:LinkButton ID="ibtnCopyorderno" runat="server" OnClick="ibtnCopyorderno_Click"><i class="fa fa-search "> </i></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtScrchcopyorderno" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                        </div>

                        <div class="col-lg-2">
                            <asp:DropDownList ID="ddlcopyorder" runat="server" CssClass="form-control form-control-sm mt20">
                            </asp:DropDownList>

                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnCopyOrder" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnCopyOrder_Click" >Copy</asp:LinkButton>

                        </div>



                    </div>
          


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

                        <div class="form-group row">
                            <div class="col-lg-3  ">
                          
                                        <asp:Label ID="comncdate" runat="server" Text="Date Of Commencement Of Work:"></asp:Label>
                                    <asp:TextBox ID="txtcomncdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcomncdat"></cc1:CalendarExtender>
                            </div>

                            <div class="col-lg-3 ">
                                        <asp:Label ID="compltdate" runat="server"  Text="Date Of Completion:"></asp:Label>
                                    <asp:TextBox ID="txtcompltdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcompltdat"></cc1:CalendarExtender>
                            </div>
                 </div>

                        <div class="form-group">

                            <div class="col-lg-12  " style="display: none;">
                                <div class="input-group">
                                    <span class="input-group-addon glypingraddon">
                                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Subject:"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt">Purchase Order For Materials</asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-6 " style="display: none;">
                                <div class="input-group">
                                    <span class="input-group-addon glypingraddon">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Dear Sir,"></asp:Label>
                                    </span>

                                </div>
                            </div>


                        </div>
                        <div class="form-group">
                            <div class="col-lg-12 " style="display: none;">
                                <div class="input-group">
                                    <span class="input-group-addon glypingraddon">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text=":"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="txtLETDES" runat="server" class="form-control inputTxt"> </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2 ">
                            </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-2">
                                        <asp:Label ID="lblNaration" runat="server" Text="Term & Condition:"></asp:Label>
                            </div>


                            <div class="col-lg-10">
                                    <asp:TextBox ID="txtTerm" runat="server" class="form-control" Rows="6" TextMode="MultiLine" Style="line-height: 18px;"></asp:TextBox>

                            </div>


                        </div>

        




            </asp:Panel>


</div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

