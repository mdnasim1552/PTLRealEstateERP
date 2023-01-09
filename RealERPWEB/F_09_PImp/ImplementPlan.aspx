<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ImplementPlan.aspx.cs" Inherits="RealERPWEB.F_09_PImp.ImplementPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
   <%-- <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

        </script>--%>



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
                    <asp:Panel ID="Panel" runat="server">



                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblProjectList" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProject" runat="server" style="width:336px" CssClass="chzn-select form-control  inputTxt" TabIndex="13" AutoPostBack="true" >
                                                
                                            </asp:DropDownList>
                                            <asp:Label ID="lblProjectDesc" runat="server" Visible="False" style="width:336px" CssClass="form-control inputTxt"></asp:Label>
                                        </div>


                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnOk1" runat="server" Style="margin-left:-50px;" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblpage" CssClass="lblTxt lblName" runat="server" Text="Page Size"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                                TabIndex="4">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class=" col-md-4  pading5px asitCol4">
                                            <asp:Label ID="lblvounotext" runat="server" CssClass="smLbl_to"
                                                Text="Implement No:"></asp:Label>


                                            <asp:Label ID="lblCurVOUNo1" runat="server" CssClass="smLbl_to"
                                                Text="WEP"></asp:Label>

                                            <asp:TextBox ID="txtCurVOUNo2" runat="server" CssClass=" inputtextbox"
                                                ReadOnly="True" TabIndex="5">000000000</asp:TextBox>

                                              <asp:Label ID="lbldate" runat="server" Text="Date:" CssClass="smLbl_to"></asp:Label>

                                            <asp:TextBox ID="txtDate" runat="server" CssClass=" inputtextbox"
                                                TabIndex="6"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                                Format="dd-MMM-yyyy " TargetControlID="txtDate"></cc1:CalendarExtender>
                                        </div>

                                     
                                        <div class=" col-md-4  pading5px  pull-right">

                                            <asp:Label ID="lblPreList" CssClass="lblTxt lblName" runat="server" Text="Prev. List"></asp:Label>
                                            <asp:TextBox ID="txtPreVouSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="lbtnPrevVOUList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnPrevVOUList_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            <asp:DropDownList ID="ddlPrevVOUList" runat="server" style="width:150px" CssClass="chzn-select form-control  inputTxt"  TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>

                                       
                                    </div>

                                    <%--<div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblPreList" CssClass="lblTxt lblName" runat="server" Text="Prev. List"></asp:Label>
                                            <asp:TextBox ID="txtPreVouSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="lbtnPrevVOUList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnPrevVOUList_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlPrevVOUList" runat="server" style="width:336px" CssClass="chzn-select form-control  inputTxt"  TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>


                                       

                                    </div>--%>


                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblpage0" CssClass="lblTxt lblName" runat="server" Text="Item Search"></asp:Label>
                                            <asp:TextBox ID="txtSearchItem" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnItemSearch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnItemSearch_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="lblmsg" CssClass=" btn btn-danger primaryBtn pull-right" runat="server" ></asp:Label>

                                        </div>

                                    </div>
                                </div>
                            </fieldset>
                        </div>


                          <div class="row">
                            <asp:Panel ID="Panel3" runat="server" Visible="false">

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-md-7 pading5px ">
                                               
                                                <asp:Label ID="lblfloorno" runat="server" CssClass="lblTxt lblName" Text="Floor No"></asp:Label>
                                                <asp:DropDownList ID="ddlfloorno" runat="server" CssClass="ddlistPull inputTxt" Width="120" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblitemList" runat="server" CssClass=" smLbl_to" Text="Item List"></asp:Label>
                                                <asp:TextBox ID="txtsrchItemName" runat="server" CssClass="inputTxt lblTxt inpPixedWidth" TabIndex="10"></asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnSearchItemList" CssClass="btn btn-primary srearchBtn" runat="server"  TabIndex="9" OnClick="imgbtnSearchItemList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                <asp:DropDownList ID="ddlitemlist" runat="server" CssClass=" chzn-select form-control inputTxt" Width="216" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlitemlist_SelectedIndexChanged">  </asp:DropDownList>
                                            
                                                 <asp:LinkButton ID="lbtnAllLab" runat="server" CssClass="btn btn-primary primaryBtn" Style="float: right !important; margin-right: 34px;margin-top: -25px !important;" OnClick="lbtnAllLab_Click">Select</asp:LinkButton>


                                            </div>
                                        </div>

                                    </div>
                                </fieldset>

                            </asp:Panel>
                        </div>

                        
                    </asp:Panel>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="847px" AllowPaging="True"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                            OnRowDeleting="gvRptResBasis_RowDeleting">
                            <PagerSettings PageButtonCount="20" Position="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />

                                <asp:TemplateField HeaderText="Floor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFloorCode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ItemCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemCode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptcod")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Description">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkfinalup" runat="server" OnClick="lnkfinalup_Click"  CssClass="btn btn-danger primaryBtn pull-left">Final Update</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDelete" runat="server"  CssClass="btn btn-primary primaryBtn pull-right"
                                                         OnClick="lnkDelete_Click">Delete All</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComQty" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" OnClick="lnktotal_Click" CssClass="btn btn-primary primaryBtn" >Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Target (System)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvstarqty" runat="server" 
                                            BorderStyle="None" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Font-Size="12px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">  
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcurqty" runat="server"  CssClass=" inputtextbox"
                                            BorderStyle="none" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Font-Size="12px" Style="text-align: right" ></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
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
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

