<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccSpecificCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSpecificCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
       


         };
         function loadModalAddCode() {
             $('#AddResCode').modal('toggle', {
                 backdrop: 'static',
                 keyboard: false
             });
         };
         function CloseModalAddCode() {
             $('#AddResCode').modal('hide');
         };

     </script>
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }
        /*.cald {
             z-index: 1;
        }*/

        .lblmargin {
            margin: 0px !important;
        }

        .lblheadertitle {
            background-color: #346CB0;
            font-size: 14px;
            font-weight: bold;
            color: white;
            padding-left: 5px !important;
        }

        .form-control-sm {
            padding: 0.25rem 0rem !important;
        }

        .grvContentarea {
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

            <div class="card card-fluid mb-1 mt-4">
                <div class="card-body">
                    <div class="row ">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="Label1" runat="server">Code Book</label>
                                <asp:LinkButton ID="lnkbtnFindProject" runat="server" > <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlMainBook" runat="server" CssClass="form-control inputTxt chzn-select " style=" width:270px;">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" CssClass="form-control "></asp:Label>
                            </div>
                        </div>                       
                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="lblprevious" runat="server">Group</label>
                                <asp:LinkButton ID="lnkbtnPrevious" runat="server" > <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="ddlUserName" id="Label3" runat="server">Details Code</label>
                               <asp:DropDownList ID="ddlCodeSegment" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                   <asp:ListItem Value="2">Main Code</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                    <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                               </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary btn-sm  margin-top30px" OnClick="lnkok_Click" >Ok</asp:LinkButton>
                            </div>

                        </div>
                    </div>

                </div>
            </div>


            <div class="card card-fluid mb-2" style="min-height: 450px;">
                <div class="card-body">
                    <div class="table-responsive mb-1" >
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" 
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15" Width="499px"
                            OnPageIndexChanging="grvacc_PageIndexChanging"  CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvacc_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                           
                            <Columns>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" Visible="False"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod3")) %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="+">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="True"?true:false %>'  OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True"  EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" " Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" Width="40px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Width="40px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf. Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="250px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px" Width="250px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                            </Columns>
                           
                              <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>


             <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New Code</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <asp:Label ID="lbgrcod" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Spec Code </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtspccode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="spcfcod2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtgvDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>   
                            <%--<div class="row mb-1">
                                <label class="col-md-4">Brand</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row mb-1">
                                <label class="col-md-4">Unit</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row mb-1">
                                <label class="col-md-4">Rate</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtrate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>--%> 
                            <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"  ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


