<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurMatIssue.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurMatIssue" %>

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

            var gridview = $('#<%=this.grvissue.ClientID %>');
            $.keynavigation(gridview);

            $('.chzn-select').chosen({ search_contains: true });

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Issue No:"></asp:Label>
                                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>
                                        <asp:TextBox ID="txtCurISSNo2" runat="server" ReadOnly="true" CssClass="inputTxt inputBox50px" TabIndex="3">000</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate">
                                        </cc1:CalendarExtender>
                                        
                                  
                                        <asp:Label ID="lblSMCR" runat="server" CssClass="smLbl_to" Text=""></asp:Label>

                                        <asp:TextBox ID="txtMIsuRef" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>
   
                                        <asp:Label ID="lblDMIR" runat="server" CssClass="lblTxt smLbl_to" Text=""></asp:Label>
                                        <asp:TextBox ID="txtsmcr" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3" Width="66px"></asp:TextBox>

                                        <div class="col-md-3 pading5px pull-right">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. Issue. List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" chzn-select" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>


                                            <asp:TextBox ID="txtsrchproject" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>


                                        <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3" style="width:385px;"></asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" Visible="False" style="width:385px;" CssClass=" smLbl_to inputTxt"></asp:Label>

                                       
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
                                
                             <%--   <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. Issue. List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage inputTxt" Width="482px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlRes" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">
                                   
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Materials"></asp:Label>
                                            <asp:TextBox ID="txtSearchMaterials" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ibtnSearchMaterisl" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSearchMaterisl_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlMaterials" runat="server" OnSelectedIndexChanged="ddlMaterials_SelectedIndexChanged" CssClass="chzn-select form-control  inputTxt" TabIndex="12" AutoPostBack="True">
                                            </asp:DropDownList>
                                            
                                        </div>

                                         <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Specification"></asp:Label>
                                            <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="chzn-select ddlistPull inputTxt" TabIndex="12" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>



                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectReaSpesAll" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lbtnSelectReaSpesAll_Click" Style="margin: 0 0 0 3px;" TabIndex="2">Select all(Mat)</asp:LinkButton>
                                        </div>
                                        
                                    </div>
                                   <div class="form-group">
                                       <div class="col-md-2 pading5px asitCol2">
                                            <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page"></asp:Label>
                                            
                                             <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18"
                                           >
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
                        <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging">
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
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvspcfcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwrkdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger primaryBtn" OnClientClick="return Confirmation();"
                                                    OnClick="lbtnDelete_Click">Delete All</asp:LinkButton>

                                            </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Specifition">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn" 
                                             OnClick="lnkupdate_Click" OnClientClick="javascript:return FunConfirmSave();">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvspecifition" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisuqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Use of location">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlocation" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "useoflocation")) %>'
                                            Width="100px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisurmk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="100px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <%--<HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />--%>
                        </asp:GridView>
                    </div>

                    <div class="row">
                        <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                             <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtISSNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
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


