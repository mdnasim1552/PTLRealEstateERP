<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="IndentMaterialRequired.aspx.cs" Inherits="RealERPWEB.F_12_Inv.IndentMaterialRequired" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $(".DeleteClick").click(function () {
                if (!confirm("Do you want to delete")) {
                    return false;
                }
            });
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
        };
    </script>
     <style>
        .aspNetDisabled {
            width: 100%;
            height: 2.25rem;
        }
        .chzn-select {
            width: 100%;
            height: 2.25rem;
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
            <div>
                <div class="row mt-3" runat="server">
                    <div class="col-12 col-lg-4 col-xl-4">
                        <div class="card">
                            <div class="card-header bg-light"><span class="font-weight-bold text-muted">Indent Material Required- Entry</span></div>
                            <div class="card-body" runat="server">
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Previous Material Required <asp:LinkButton ID="lnkPrvList" OnClick="lnkPrvList_Click" runat="server"><i class="fa fa-search"> </i></asp:LinkButton>
                                    </label>
                                    <asp:DropDownList ID="ddlPreList" OnSelectedIndexChanged="ddlStoreList_SelectedIndexChanged" AutoPostBack="true" class="chzn-select form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="row">
                                    <div class="form-group pl-0 col-4">
                                        <label for="ddlLvType">
                                            Date  
                                        </label>
                                        <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" ReadOnly="true" class="form-control disabled"></asp:TextBox>
                                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtaplydate"></cc1:CalendarExtender>--%>
                                    </div>
                                    <div class="form-group col-4 pr-0">

                                        <asp:Label ID="reqNo" CssClass="d-block" runat="server">ID No</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="btn btn-sm btn-secsondary mt-2 pl-0">IND-</asp:Label>
                                        <asp:Label ID="txtCurNo2" runat="server" CssClass="btn btn-sm btn-secsondary mt-2 pr-0">000</asp:Label>




                                    </div>
                                    <div class="form-group col-4 pr-0">
                                        <label for="ddlLvType">
                                            Ref No. #  
                                             <asp:RequiredFieldValidator ID="refno" runat="server" ControlToValidate="txtrefno" ErrorMessage="*" SetFocusOnError="true"   
ForeColor="Red"></asp:RequiredFieldValidator>  
                                        </label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Store Name  
                                    </label>
                                    <asp:DropDownList ID="ddlStoreList" OnSelectedIndexChanged="ddlStoreList_SelectedIndexChanged" Enabled="false" AutoPostBack="true" class="chzn-select form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Department <span class="text-danger">*</span>
                                    </label>
                                    <asp:DropDownList ID="ddlDeptCode" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" class="chzn-select form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group text-right">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-success" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>

                                <div class="form-group" id="divMatrial" runat="server" visible="false">
                                    <label for="ddlLvType">
                                        Materials
                                       
                                    </label>
                                    <asp:DropDownList ID="ddlMaterials" runat="server" OnSelectedIndexChanged="ddlMaterials_SelectedIndexChanged" AutoPostBack="true" class="chzn-select form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group" id="divMatrialSpec" runat="server" visible="false">
                                    <label for="ddlLvType">
                                        Specification
                                    </label>
                                    <asp:DropDownList ID="ddlResSpcf" runat="server" OnSelectedIndexChanged="ddlResSpcf_SelectedIndexChanged" AutoPostBack="true" class="chzn-select form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group text-right" id="divBtn" runat="server" visible="false">
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="pr-1 btn btn-sm btn-primary" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="pr-1 btn btn-sm btn-primary" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="col-12 col-lg-8 col-xl-8">
                        <div class="card">
                            <div class="card-header bg-light"><span class="font-weight-bold text-muted">Added Material's</span></div>
                            <div class="card-body" runat="server">
                                <div class="row">
                                    <asp:GridView ID="gvIssue" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                        PageSize="15">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.#">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
<%--                                            <asp:CommandField ShowDeleteButton="True" ItemStyle-ForeColor="Red"  DeleteText='<i class="fa fa-trash"> </i>' />--%>
                                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                   <asp:LinkButton runat="server" ID="lblDelete" CssClass="text-red" OnClick="lblDelete_Click"  ><i class="fa fa-trash"> </i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                      

                                            <asp:TemplateField HeaderText="Resource Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server"
                                                        Style="font-size: 11px; text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvissueqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'></asp:TextBox>

                                                </ItemTemplate>
                                                
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
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
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

