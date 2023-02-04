<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="WrkOrderEdit.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.WrkOrderEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvpurorder.ClientID %>');
            gv1.Scrollable();
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });




        };


        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
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
                                        <asp:Label ID="lCurReqdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="  inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>



                                    </div>

                                </div>

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Requisition"></asp:Label>
                                            <asp:TextBox ID="txtSrcRequisition01" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnFindReqno01" runat="server" CssClass="btn btn-primary srearchBtn" TabIndex="2" OnClick="imgbtnFindReqno01_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px  asitCol4">
                                            <asp:DropDownList ID="ddlReqNo01" runat="server" CssClass="form-control inputTxt" Style="width: 336px;">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 pading5px  asitCol1">

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                            <asp:Label ID="lblbillno" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

                                        </div>
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvpurorder" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea" OnRowCancelingEdit="gvpurorder_RowCancelingEdit" OnRowEditing="gvpurorder_RowEditing" OnRowUpdating="gvpurorder_RowUpdating" OnRowDataBound="gvpurorder_RowDataBound">

                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Group" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvGroup" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Reqno" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcodo")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="140px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelMat" OnClick="btnDelMat_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                </ItemTemplate>


                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref. No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnqtyTotal" runat="server"
                                        CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnqtyTotal_Click">Total :</asp:LinkButton>
                                </FooterTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />


                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                        CssClass="btn btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="25px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:CommandField CancelText="Can" ShowEditButton="True" />

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>


                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlspecification" runat="server" Width="120px" CssClass="chzn-select">
                                    </asp:DropDownList>
                                </EditItemTemplate>


                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                        Width="140px"></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSupname" runat="server" Width="150px" CssClass="chzn-select">
                                    </asp:DropDownList>
                                </EditItemTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px" BorderStyle="none" OnTextChanged="txtgvreqty01_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Supplier Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsuprate" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Actual Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvAppRate01" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                Width="120px"></asp:Label></ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>
                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>




                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


