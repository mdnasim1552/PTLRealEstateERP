<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UploadDocumentation.aspx.cs" Inherits="RealERPWEB.F_33_Doc.UploadDocumentation" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>


                   
    <script src="../assets/js/ckeditor/ckeditor.js"></script>

    <script type="text/javascript">



        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $(function () {
                CKEDITOR.replace('<%=txtDetails1.ClientID %>');
            });
            $(function () {
                 // Replace the <textarea id="editor1"> with a CKEditor
                // instance, using default configuration.
                // CKEDITOR.replace('<%=txtDetails1.ClientID%>');  //Nahiod comments 021020019
                //bootstrap WYSIHTML5 - text editor
                //$('.textarea').wysihtml5()
            })
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
            <div class="card mt-5">

                <div class="card-body">
                    <div class="row">
       
                        <div class="col-lg-6">
                                          <form>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" Text="Type:"></asp:Label>
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>





                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbltitle" runat="server" Text="Title :"></asp:Label>

                                        <asp:Panel runat="server" ID="pnlTxt">
                                            <asp:TextBox ID="txtsName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="txtsName" ValidationGroup="group1" ErrorMessage="Please enter a title!" />
                                        </asp:Panel>


                                        <asp:Panel ID="pnlMonth" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlDept" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:Panel>

                                    </div>
                                </div>


                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblimg" runat="server" Text="Documents:"></asp:Label>
                                        <asp:FileUpload ID="imgFileUpload" CssClass="form-control" runat="server" AllowMultiple="true" accept=".pdf"/>
  <%--                                      <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="imgFileUpload" ValidationGroup="group1" ErrorMessage="Please enter an image" />--%>
                                    </div>
                                </div>

                                
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" Text="Details:"></asp:Label>
                                        <div id="summernote"></div>
                                        <asp:TextBox ID="txtDetails1" runat="server" Rows="5" CssClass="form-control " TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <p class="text-right">
                                            <asp:LinkButton ID="lnk_save" ValidationGroup="group1" CssClass="btn btn-success btn-sm mt20" runat="server" OnClick="lnk_save_Click">Save</asp:LinkButton>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            </form>
                        </div>

                        <div class="col-lg-6">
                            <div class="card">
                                <div class="card-header bg-light">
                                                              <div>
                                        <asp:RadioButtonList ID="datatype" runat="server" AutoPostBack="True"
                                            CssClass="custom-control custom-control-inline custom-checkbox rbt p-0"
                                            Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            OnSelectedIndexChanged="datatype_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                          <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Code of conduct</asp:ListItem>
                                            <asp:ListItem>Organogram</asp:ListItem>
                                             <asp:ListItem>Winner List</asp:ListItem>
                                             <asp:ListItem>HR Policy</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="card-body">
                                             <div class="table table-sm table-responsive">
                                <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvdoc" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltitle" runat="server" Text='<%#Eval("title")%>' Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" runat="server" Text='<%#Eval("remarks").ToString().Substring(1,30)%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File">
                                            <ItemTemplate>
                                                <asp:Label ID="lblimgpath" runat="server" Text='<%#Eval("imgpath")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblid" runat="server" Text='<%#Eval("id")%>' Visible="false"></asp:Label>


                                                <asp:HyperLink runat="server" CssClass="btn btn-primary btn-sm text-white" NavigateUrl='<%#Eval("imgpath")%>' Target="_blank"><i class="fa fa-eye"></i> </asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                               <ItemTemplate>
                                                  <asp:LinkButton ID="btn_edit" runat="server" CssClass="btn-sm text-info" OnClick="btn_edit_Click" > <i class="fa fa-edit"></i> 
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn-sm text-danger" OnClick="btn_remove_Click"> <i class="fa fa-trash"></i> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                            </div>
                   
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="lnk_save" />
        </Triggers>
    </asp:UpdatePanel>
     <script>
         $(function () {
             CKEDITOR.replace('<%=txtDetails1.ClientID %>');
            });
            $(function () {
                 // Replace the <textarea id="editor1"> with a CKEditor
                // instance, using default configuration.
                // CKEDITOR.replace('<%=txtDetails1.ClientID%>');  //Nahiod comments 021020019
                //bootstrap WYSIHTML5 - text editor
                //$('.textarea').wysihtml5()
            })
     </script>

</asp:Content>



