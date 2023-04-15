<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMatPurHistory.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptMatPurHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" style="width:336px" CssClass="chzn-select form-control  inputTxt"  OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Material Name</asp:Label>
                                            <asp:TextBox ID="txtSrcMat" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>



                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgBtnMatName" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgBtnMatName_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlMaterialName" runat="server" style="width:336px" CssClass="chzn-select form-control  inputTxt">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-1 pading5px">

                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-left:-50px;" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="lblUnit" runat="server" Visible="False" Width="150px"></asp:Label>
                                        </div>


                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-5 pading5px">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">From</asp:Label>


                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                            </cc1:CalendarExtender>
                                            <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt smLbl_to" style="margin-right:7px;">To</asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt smLbl">Page</asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="80px">
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




                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvMatPurHis" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="930px"
                            OnPageIndexChanging="gvMatPurHis_PageIndexChanging">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MRR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MRR Ref.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrefno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MRR Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrderDate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MRR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MRF No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqrefno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                     <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Description" Width="110px" ></asp:Label>
                                                 <asp:HyperLink ID="hlbtngvMatPurHisExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprojectdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSupName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvSupName" runat="server" Font-Bold="True" Font-Size="12px" Text="Total: "
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Brand Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBrName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MRR Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvMRRQty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




